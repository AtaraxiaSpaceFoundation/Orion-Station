// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class EmoteChatMessageDetectedEvent : EntityEventArgs
{
    public EmoteChatMessageDetectedEvent(EntityUid source, string action, bool voluntary)
    {
        Source = source;
        Action = action;
        Voluntary = voluntary;
    }

    public EntityUid Source { get; }
    public string Action { get; }
    public bool Voluntary { get; }
}
