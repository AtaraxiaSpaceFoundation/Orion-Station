// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Diagnostics.CodeAnalysis; // Orion
using Robust.Shared.Player;

namespace Content.Shared.Players.PlayTimeTracking;

public interface ISharedPlaytimeManager
{
    /// <summary>
    /// Gets the playtimes for the session or an empty dictionary if none found.
    /// </summary>
    IReadOnlyDictionary<string, TimeSpan> GetPlayTimes(ICommonSession session);

    bool TryGetTrackerTimes(ICommonSession id, [NotNullWhen(true)] out IReadOnlyDictionary<string, TimeSpan>? time); // Orion
}
