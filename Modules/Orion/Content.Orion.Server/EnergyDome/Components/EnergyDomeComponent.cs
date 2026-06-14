// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 TheShuEd <96445749+theshued@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Orion.Server.EnergyDome.Systems;
using Robust.Shared.Analyzers;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Server.EnergyDome.Components;

/// <summary>
/// Allows linking the dome generator with the dome itself
/// </summary>
[RegisterComponent, Access(typeof(EnergyDomeSystem))]
public sealed partial class EnergyDomeComponent : Component
{
    /// <summary>
    /// Linked generator that uses energy
    /// </summary>
    [DataField]
    public EntityUid? Generator;
}
