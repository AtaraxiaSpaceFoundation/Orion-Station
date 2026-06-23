// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Player;

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class CVarChangedByCommandEvent : EntityEventArgs
{
    public CVarChangedByCommandEvent(string name, ICommonSession? actor, object? oldValue, object? newValue)
    {
        Name = name;
        Actor = actor;
        OldValue = oldValue;
        NewValue = newValue;
    }

    public string Name { get; }
    public ICommonSession? Actor { get; }
    public object? OldValue { get; }
    public object? NewValue { get; }
}
