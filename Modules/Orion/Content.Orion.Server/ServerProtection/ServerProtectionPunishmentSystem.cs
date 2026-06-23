// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Net;
using System.Net.Sockets;
using Content.Server.Administration;
using Content.Server.Administration.Managers;
using Content.Server.Administration.Systems;
using Content.Server.Chat.Managers;
using Content.Server.GameTicking;
using Content.Server.GhostKick;
using Content.Server.Mind;
using Content.Shared.Database;
using Robust.Server.Player;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Network;
using Robust.Shared.Player;

namespace Content.Orion.Server.ServerProtection;

public sealed partial class ServerProtectionPunishmentSystem : EntitySystem
{
    [Dependency] private IBanManager _banManager = default!;
    [Dependency] private IPlayerLocator _locator = default!;
    [Dependency] private IAdminManager _adminManager = default!;
    [Dependency] private IChatManager _chat = default!;
    [Dependency] private GhostKickManager _ghostKickManager = default!;
    [Dependency] private IPlayerManager _playerManager = default!;

    private ISawmill _log = default!;

    private MindSystem Minds => EntityManager.System<MindSystem>();
    private GameTicker GameTicker => EntityManager.System<GameTicker>();

    public override void Initialize()
    {
        base.Initialize();
        _log = Logger.GetSawmill("serverprotection.actions");
    }

    /// <summary>
    /// Applies a ban to the player.
    /// </summary>
    public async void ApplyBan(ICommonSession player, string reason, int durationMinutes = 0)
    {
        if (string.IsNullOrWhiteSpace(player.Name))
            return;

        (IPAddress, int)? targetIP = null;
        ImmutableTypedHwid? targetHWid = null;

        var sessionData = await _locator.LookupIdAsync(player.UserId);
        if (sessionData != null)
        {
            if (sessionData.LastAddress is not null)
            {
                var prefix = sessionData.LastAddress.AddressFamily == AddressFamily.InterNetwork ? 32 : 64;
                targetIP = (sessionData.LastAddress, prefix);
            }

            targetHWid = sessionData.LastHWId;
        }

        uint? expires = durationMinutes <= 0 ? null : (uint)durationMinutes;

        var banInfo = new CreateServerBanInfo(reason);
        banInfo.WithSeverity(NoteSeverity.High);
        banInfo.WithBanningAdmin(null);
        banInfo.AddUser(player.UserId, player.Name);
        if (targetIP is { } addressRange)
            banInfo.AddAddressRange(addressRange);
        if (targetHWid is { } hwid)
            banInfo.AddHWId(hwid);
        if (expires is { } minutes)
            banInfo.WithMinutes(minutes);

        _banManager.CreateServerBan(banInfo);

        _log.Info($"{player.Name} был забанен: {reason}");
    }

    /// <summary>
    /// Applies a ban to a player by user id.
    /// </summary>
    public async void ApplyBan(NetUserId userId, string? username, string reason, int durationMinutes = 0)
    {
        (IPAddress, int)? targetIP = null;
        ImmutableTypedHwid? targetHWid = null;

        var sessionData = await _locator.LookupIdAsync(userId);
        if (sessionData != null)
        {
            if (sessionData.LastAddress is not null)
            {
                var prefix = sessionData.LastAddress.AddressFamily == AddressFamily.InterNetwork ? 32 : 64;
                targetIP = (sessionData.LastAddress, prefix);
            }

            targetHWid = sessionData.LastHWId;
        }

        uint? expires = durationMinutes <= 0 ? null : (uint) durationMinutes;

        var banInfo = new CreateServerBanInfo(reason);
        banInfo.WithSeverity(NoteSeverity.High);
        banInfo.WithBanningAdmin(null);
        banInfo.AddUser(userId, username ?? userId.ToString());
        if (targetIP is { } addressRange)
            banInfo.AddAddressRange(addressRange);
        if (targetHWid is { } hwid)
            banInfo.AddHWId(hwid);
        if (expires is { } minutes)
            banInfo.WithMinutes(minutes);

        _banManager.CreateServerBan(banInfo);

        _log.Info($"{username ?? userId.ToString()} был забанен: {reason}");
    }

    /// <summary>
    /// De-admins a player session.
    /// </summary>
    public void DeAdmin(ICommonSession player, string reason)
    {
        _adminManager.DeAdmin(player);
        _log.Warning($"{player.Name} был deadmin: {reason}");
    }

    /// <summary>
    /// Kicks the player.
    /// </summary>
    public void KickPlayer(ICommonSession player, string reason)
    {
        _ghostKickManager.DoDisconnect(player.Channel, reason);
        _log.Info($"{player.Name} был кикнут: {reason}");
    }

    /// <summary>
    /// Deletes all messages sent by the player.
    /// </summary>
    public void DeleteMessages(ICommonSession player)
    {
        _chat.DeleteMessagesBy(player.UserId);
    }

    /// <summary>
    /// Erases the current character entity and respawns the player as an observer.
    /// </summary>
    public void EraseCharacter(ICommonSession player)
    {
        if (!Minds.TryGetMind(player.UserId, out var mindId, out var mind) ||
            mind.OwnedEntity is not { } entity ||
            TerminatingOrDeleted(entity))
        {
            var eraseEvent = new EraseEvent(player.UserId);
            RaiseLocalEvent(ref eraseEvent);
            return;
        }

        Minds.WipeMind(mindId, mind);
        QueueDel(entity);

        if (_playerManager.TryGetSessionById(player.UserId, out var session))
            GameTicker.SpawnObserver(session);

        var eraseEventLocal = new EraseEvent(player.UserId);
        RaiseLocalEvent(ref eraseEventLocal);
    }

    /// <summary>
    /// Sends an admin alert.
    /// </summary>
    public void SendAdminAlert(string message)
    {
        _chat.SendAdminAlert(message);
    }
}
