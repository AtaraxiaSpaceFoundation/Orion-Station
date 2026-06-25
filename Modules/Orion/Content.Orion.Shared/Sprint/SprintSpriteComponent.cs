// SPDX-FileCopyrightText: 2026 ReWAFFlution <rockstarfly65@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Shared.Sprint;

/// <summary>
/// Tracks the visual sprint sprite timer on the player entity.
/// The system spawns a visual effect every <see cref="SpawnInterval"/> seconds while sprinting.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class SprintSpriteComponent : Component
{
    /// <summary>
    /// Interval in seconds between sprite spawns.
    /// </summary>
    [DataField]
    public float SpawnInterval = 0.6f;

    /// <summary>
    /// Time accumulated since the last sprite spawn.
    /// </summary>
    public float TimeSinceLastSpawn;

    /// <summary>
    /// Prototype ID of the visual effect entity to spawn.
    /// Must be an entity prototype with a sprite and optional <see cref="Robust.Shared.Spawners.TimedDespawnComponent"/>.
    /// </summary>
    [DataField]
    public string EntProtoId = "OrionSprintEffect";
}
