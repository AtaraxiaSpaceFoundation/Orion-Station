// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using Content.Server._Orion.ServerProtection.Events;
using Content.Orion.Shared.CCVar;
using Content.Shared.Administration.Managers;
using Content.Shared.Speech;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Log;
using Robust.Shared.Random;

namespace Content.Orion.Server.ServerProtection.Emoting;

public sealed partial class EmoteProtectionSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private IPlayerManager _playerManager = default!;
    [Dependency] private ISharedAdminManager _admin = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private ServerProtectionPunishmentSystem _punishment = default!;
    [Dependency] private ServerProtectionAuditManager _toggleAudit = default!;

    private ISawmill _log = default!;

    private bool _protectionEnabled;
    private bool _eraseEnabled;
    private bool _banEnabled;
    private bool _kickEnabled;
    private bool _deleteMessagesEnabled;
    private int _banDuration;
    private bool _initialized;

    private int _hardEmoteThreshold;
    private int _softThresholdVariance;
    private float _postSoftThresholdProbability;
    private float _softThresholdRefreshCooldown;
    private float _clearInterval;

    private int? _softThreshold;
    private readonly Dictionary<EntityUid, int> _emoteTracker = new();
    private float _timeSinceLastClear;
    private float _timeSinceLastRefresh;

    public override void Initialize()
    {
        base.Initialize();

        _log = Logger.GetSawmill("serverprotection.emote_protection");

        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionEnabled, OnProtectionEnabledChanged, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionEraseEnabled, v => _eraseEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionBanEnabled, v => _banEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionKickEnabled, v => _kickEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionDeleteMessages, v => _deleteMessagesEnabled = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionBanDuration, v => _banDuration = v, true);

        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionHardThreshold, v => _hardEmoteThreshold = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionSoftVariance, v => _softThresholdVariance = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionPostSoftProbability, v => _postSoftThresholdProbability = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionSoftRefreshCooldown, v => _softThresholdRefreshCooldown = v, true);
        _cfg.OnValueChanged(OrionCCVars.EmoteProtectionClearInterval, v => _clearInterval = v, true);

        SubscribeLocalEvent<EmoteChatMessageDetectedEvent>(OnEmoteDetected);

        _initialized = true;
    }

    private void OnProtectionEnabledChanged(bool enabled)
    {
        var old = _protectionEnabled;
        _protectionEnabled = enabled;

        if (!_initialized || old == enabled)
            return;

        var actor = _toggleAudit.TryGetRecentActor(OrionCCVars.EmoteProtectionEnabled.Name, TimeSpan.FromSeconds(5), out var knownActor)
            ? knownActor
            : "unknown";

        var state = enabled ? "включена" : "ВЫКЛЮЧЕНА";
        var message = $"[ServerProtection] Система EmoteProtection была {state}. Переключил: {actor}.";
        _punishment.SendAdminAlert(message);
        _log.Info(message);
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        _timeSinceLastClear += frameTime;
        _timeSinceLastRefresh += frameTime;

        if (_timeSinceLastClear >= _clearInterval)
        {
            _emoteTracker.Clear();
            _timeSinceLastClear = 0f;
        }

        if (!(_timeSinceLastRefresh >= _softThresholdRefreshCooldown))
            return;

        GetSoftThreshold(true);
        _timeSinceLastRefresh = 0f;
    }

    private void OnEmoteDetected(EmoteChatMessageDetectedEvent ev)
    {
        OnEmoteDetected(ev.Source, ev.Voluntary);
    }

    private void OnEmoteDetected(EntityUid uid, bool voluntary)
    {
        if (!_protectionEnabled || !voluntary)
            return;

        if (!TryComp<SpeechComponent>(uid, out _))
            return;

        if (!_playerManager.TryGetSessionByEntity(uid, out _)) // Don't ban NPCs
            return;

        if (_admin.IsAdmin(uid, true))
            return;

        Add(uid);
    }

    private void Add(EntityUid uid)
    {
        var count = _emoteTracker.GetValueOrDefault(uid) + 1;
        _emoteTracker[uid] = count;

        TryHardThresholdViolation(uid, count);
        TrySoftThresholdViolation(uid, count);
    }

    private void TryHardThresholdViolation(EntityUid uid, int count)
    {
        if (count >= Math.Max(1, _hardEmoteThreshold))
            HandleViolation(uid, "hard", count.ToString());
    }

    private void TrySoftThresholdViolation(EntityUid uid, int count)
    {
        var soft = GetSoftThreshold();

        if (count < soft)
            return;

        var steps = count - soft;
        var chance = Math.Clamp(steps * _postSoftThresholdProbability, 0f, 1f);

        if (_random.Prob(chance))
            HandleViolation(uid, "soft", count.ToString());
    }

    private int GetSoftThreshold(bool refresh = false)
    {
        if (_softThreshold != null && !refresh)
            return _softThreshold.Value;

        var baseThreshold = Math.Max(1, _hardEmoteThreshold) * 3 / 4;
        var randomReduction = _random.Next(0, Math.Max(1, _softThresholdVariance));
        _softThreshold = Math.Max(2, baseThreshold - randomReduction);

        return _softThreshold.Value;
    }

    private void HandleViolation(EntityUid uid, string emoteText, string count)
    {
        if (!_playerManager.TryGetSessionByEntity(uid, out var session))
            return;

        var word = emoteText.Length > 30
            ? emoteText[..30] + "..."
            : emoteText;
        var banReason = Loc.GetString("emote-protection-ban-reason", ("word", word), ("count", count));
        var kickReason = Loc.GetString("emote-protection-kick-reason", ("word", word), ("count", count));

        _log.Info($"{session.Name} ({session.UserId}) превысил лимит эмоций: {count} раз!");

        if (_deleteMessagesEnabled)
            _punishment.DeleteMessages(session);

        if (_eraseEnabled)
            _punishment.EraseCharacter(session);

        if (_banEnabled)
        {
            _punishment.SendAdminAlert(Loc.GetString("emote-protection-admin-announcement-ban-reason",
                ("player", session.Name),
                ("word", word),
                ("count", count)));
            _punishment.ApplyBan(session, banReason, _banDuration);
        }
        else if (_kickEnabled)
        {
            _punishment.SendAdminAlert(Loc.GetString("emote-protection-admin-announcement-kick-reason",
                ("player", session.Name),
                ("word", word),
                ("count", count)));
            _punishment.KickPlayer(session, kickReason);
        }

        _emoteTracker.Remove(uid);
    }
}
