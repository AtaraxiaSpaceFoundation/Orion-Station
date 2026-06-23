// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using Content.Server._Orion.ServerProtection.Events;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Player;
using Robust.Shared.Timing;

namespace Content.Orion.Server.ServerProtection;

public sealed partial class ServerProtectionAuditManager : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;

    private readonly Dictionary<string, (string Actor, TimeSpan ChangedAt)> _changes = new();

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CVarChangedByCommandEvent>(OnCVarChangedByCommand);
    }

    private void OnCVarChangedByCommand(CVarChangedByCommandEvent ev)
    {
        if (!ev.Name.StartsWith("protection.", StringComparison.OrdinalIgnoreCase))
            return;

        RecordChange(ev.Name, ev.Actor, ev.OldValue, ev.NewValue);
    }

    private void RecordChange(string cvarName, ICommonSession? actor, object? oldValue, object? newValue)
    {
        if (Equals(oldValue, newValue))
            return;

        var actorInfo = actor == null
            ? "unknown"
            : $"{actor.Name} ({actor.UserId})";

        _changes[cvarName] = (actorInfo, _timing.CurTime);
    }

    public bool TryGetRecentActor(string cvarName, TimeSpan maxAge, out string actor)
    {
        if (_changes.TryGetValue(cvarName, out var change) && _timing.CurTime - change.ChangedAt <= maxAge)
        {
            actor = change.Actor;
            return true;
        }

        actor = "unknown";
        return false;
    }
}
