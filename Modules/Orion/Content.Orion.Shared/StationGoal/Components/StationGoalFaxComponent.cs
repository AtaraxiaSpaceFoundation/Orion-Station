// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Shared.StationGoal.Components;

/// <summary>
/// Marks a fax as eligible to receive station goals.
/// </summary>
[RegisterComponent]
public sealed partial class StationGoalFaxComponent : Component
{
    /// <summary>
    /// Should this fax receive goals for its owning station.
    /// </summary>
    [DataField]
    public bool ReceiveStationGoal { get; set; }

    /// <summary>
    /// Should this fax receive goals for every station.
    /// </summary>
    [DataField]
    public bool ReceiveAllStationGoals { get; set; }
}
