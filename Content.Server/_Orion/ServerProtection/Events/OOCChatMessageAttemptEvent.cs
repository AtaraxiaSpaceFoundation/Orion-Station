// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Player;

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class OOCChatMessageAttemptEvent : CancellableEntityEventArgs
{
    public OOCChatMessageAttemptEvent(string message, ICommonSession player)
    {
        Message = message;
        Player = player;
    }

    public string Message { get; }
    public ICommonSession Player { get; }
}
