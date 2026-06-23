// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 ThereDrD <88589686+theredrd0@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Maths;
using Robust.Shared.Serialization.Manager.Attributes;
using Robust.Shared.Utility;
using static Robust.Shared.Utility.SpriteSpecifier;

namespace Content.Orion.Shared.Lighting.Shaders;

/// <summary>
/// Adds a client-side glow mask that is composited over matching point lights.
/// </summary>
[RegisterComponent, NetworkedComponent]
public sealed partial class LightingOverlayComponent : Component
{
    [DataField]
    public bool? Enabled;

    [DataField]
    public SpriteSpecifier Sprite = new Texture(new ResPath("_Orion/Effects/LightMasks/lightmask_lamp.png"));

    [DataField]
    public float OffsetX;

    [DataField]
    public float OffsetY = 0.5f;

    [DataField]
    public Color? Color;
}
