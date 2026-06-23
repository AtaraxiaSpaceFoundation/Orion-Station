// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class ICChatMessageAttemptEvent : CancellableEntityEventArgs
{
    public ICChatMessageAttemptEvent(string message, EntityUid source)
    {
        Message = message;
        Source = source;
    }

    public string Message { get; }
    public EntityUid Source { get; }
}
