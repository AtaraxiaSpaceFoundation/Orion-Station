// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Linq;
using Content.Server._Orion.ServerProtection.Events;
using Content.Orion.Shared.CCVar;
using Content.Orion.Shared.ServerProtection.Chat;
using Content.Shared.Administration.Managers;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;

namespace Content.Orion.Server.ServerProtection.Chat;

public sealed partial class ChatProtectionSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private IPrototypeManager _proto = default!;
    [Dependency] private IPlayerManager _playerManager = default!;
    [Dependency] private ISharedAdminManager _admin = default!;
    [Dependency] private ServerProtectionPunishmentSystem _punishment = default!;
    [Dependency] private ServerProtectionAuditManager _toggleAudit = default!;

    private ISawmill _log = default!;
    private readonly HashSet<string> _icWords = new();
    private readonly HashSet<string> _oocWords = new();

    private bool _protectionEnabled;
    private bool _eraseEnabled;
    private bool _banEnabled;
    private bool _kickEnabled;
    private bool _deleteMessagesEnabled;
    private int _banDuration;
    private bool _cacheDone;
    private bool _initialized;

    public override void Initialize()
    {
        base.Initialize();

        _log = Logger.GetSawmill("serverprotection.chat_protection");
        _proto.PrototypesReloaded += OnPrototypesReloaded;

        _cfg.OnValueChanged(OrionCCVars.ChatProtectionEnabled, OnProtectionEnabledChanged, true);
        _cfg.OnValueChanged(OrionCCVars.ChatProtectionEraseEnabled, v => _eraseEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.ChatProtectionBanEnabled, v => _banEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.ChatProtectionKickEnabled, v => _kickEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.ChatProtectionDeleteMessages, v => _deleteMessagesEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.ChatProtectionBanDuration, v => _banDuration = v, true);

        SubscribeLocalEvent<ICChatMessageAttemptEvent>(OnICMessageAttempt);
        SubscribeLocalEvent<OOCChatMessageAttemptEvent>(OnOOCMessageAttempt);

        _initialized = true;
    }

    private void OnProtectionEnabledChanged(bool enabled)
    {
        var old = _protectionEnabled;
        _protectionEnabled = enabled;

        if (!_initialized || old == enabled)
            return;

        var actor = _toggleAudit.TryGetRecentActor(OrionCCVars.ChatProtectionEnabled.Name, TimeSpan.FromSeconds(5), out var knownActor)
            ? knownActor
            : "unknown";

        var state = enabled ? "включена" : "ВЫКЛЮЧЕНА";
        var message = $"[ServerProtection] Система ChatProtection была {state}. Переключил: {actor}.";
        _punishment.SendAdminAlert(message);
        _log.Info(message);
    }

    private void CachePrototypes()
    {
        _icWords.Clear();
        _oocWords.Clear();

        foreach (var proto in _proto.EnumeratePrototypes<ChatProtectionListPrototype>())
        {
            switch (proto.ID) // Handled by "Prototypes/_Orion/chat_protection.yml"
            {
                case "IC_BannedWords":
                    foreach (var word in proto.Words)
                    {
                        _icWords.Add(word);
                    }

                    break;

                case "OOC_BannedWords":
                    foreach (var word in proto.Words)
                    {
                        _oocWords.Add(word);
                    }

                    break;
            }
        }

        _cacheDone = true;
        _log.Info($"Кэшировано {_icWords.Count} IC и {_oocWords.Count} OOC запрещённых слов.");
    }

    private void OnPrototypesReloaded(PrototypesReloadedEventArgs args)
    {
        CachePrototypes();
    }

    private void OnICMessageAttempt(ICChatMessageAttemptEvent ev)
    {
        if (CheckICMessage(ev.Message, ev.Source))
            ev.Cancel();
    }

    private void OnOOCMessageAttempt(OOCChatMessageAttemptEvent ev)
    {
        if (CheckOOCMessage(ev.Message, ev.Player))
            ev.Cancel();
    }

    private bool CheckICMessage(string message, EntityUid player)
    {
        if (!_protectionEnabled || string.IsNullOrEmpty(message))
            return false;

        if (!_playerManager.TryGetSessionByEntity(player, out var session))
            return false;

        if (_admin.IsAdmin(player, true))
           return false;

        if (!_cacheDone) // Something like initialization for prototypes
            CachePrototypes();

        foreach (var word in _icWords.Where(word => message.Contains(word, StringComparison.OrdinalIgnoreCase)))
        {
            HandleViolation(session, word, "IC");
            return true;
        }

        return false;
    }

    private bool CheckOOCMessage(string message, ICommonSession session)
    {
        if (!_protectionEnabled || string.IsNullOrEmpty(message))
            return false;

        if (_admin.IsAdmin(session, true))
            return false;

        if (!_cacheDone) // Something like initialization for prototypes
            CachePrototypes();

        foreach (var word in _oocWords.Where(word => message.Contains(word, StringComparison.OrdinalIgnoreCase)))
        {
            HandleViolation(session, word, "OOC");
            return true;
        }

        return false;
    }

    private void HandleViolation(ICommonSession player, string word, string channel)
    {
        var banReason = Loc.GetString("chat-protection-ban-reason", ("word", word), ("channel", channel));
        var kickReason = Loc.GetString("chat-protection-kick-reason", ("word", word), ("channel", channel));
        _log.Info($"{player.Name} ({player.UserId}) использовал запрещённое слово: '{word}' в {channel}");

        if (_deleteMessagesEnabled)
            _punishment.DeleteMessages(player);

        if (channel == "IC" && _eraseEnabled)
            _punishment.EraseCharacter(player);

        if (_banEnabled)
        {
            _punishment.SendAdminAlert(Loc.GetString("chat-protection-admin-announcement-ban-reason",
                ("player", player.Name),
                ("word", word),
                ("channel", channel)));
            _punishment.ApplyBan(player, banReason, _banDuration);
        }
        else if (_kickEnabled)
        {
            _punishment.SendAdminAlert(Loc.GetString("chat-protection-admin-announcement-kick-reason",
                ("player", player.Name),
                ("word", word),
                ("channel", channel)));
            _punishment.KickPlayer(player, kickReason);
        }
    }
}
