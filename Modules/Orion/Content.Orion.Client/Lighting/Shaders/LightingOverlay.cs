// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 ThereDrD <88589686+theredrd0@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Numerics;
using Content.Orion.Shared.CCVar;
using Content.Orion.Shared.Lighting.Shaders;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Configuration;
using Robust.Shared.Enums;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;
using DrawDepth = Content.Shared.DrawDepth.DrawDepth;

namespace Content.Orion.Client.Lighting.Shaders;

public sealed class LightingOverlay : Overlay
{
    private readonly EntityManager _entityManager;
    private readonly SpriteSystem _spriteSystem;
    private readonly TransformSystem _transformSystem;
    private readonly IConfigurationManager _cfg;
    private readonly ShaderInstance _shader;
    private readonly Action<bool> _lightCvarChanged;
    private bool _enableGlowing;

    public override OverlaySpace Space => OverlaySpace.WorldSpaceEntities;
    public override bool RequestScreenTexture => true;

    private static readonly ProtoId<ShaderPrototype> LightingOverlayShader = "LightingOverlay";

    public LightingOverlay(EntityManager entityManager, IPrototypeManager prototypeManager)
    {
        _entityManager = entityManager;
        _spriteSystem = entityManager.EntitySysManager.GetEntitySystem<SpriteSystem>();
        _transformSystem = entityManager.EntitySysManager.GetEntitySystem<TransformSystem>();
        _cfg = IoCManager.Resolve<IConfigurationManager>();
        _lightCvarChanged = value => _enableGlowing = value;
        _cfg.OnValueChanged(OrionCCVars.EnableLightsGlowing, _lightCvarChanged, true);

        _shader = prototypeManager.Index(LightingOverlayShader).InstanceUnique();
        ZIndex = (int) DrawDepth.Overdoors;
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (!_enableGlowing || ScreenTexture == null)
            return;

        var xformQuery = _entityManager.GetEntityQuery<TransformComponent>();
        var handle = args.WorldHandle;
        var bounds = args.WorldAABB.Enlarged(5f);

        _shader.SetParameter("SCREEN_TEXTURE", ScreenTexture);
        handle.UseShader(_shader);

        var query = _entityManager.AllEntityQueryEnumerator<LightingOverlayComponent, PointLightComponent, TransformComponent>();
        while (query.MoveNext(out _, out var component, out var pointLight, out var xform))
        {
            if (xform.MapID != args.MapId)
                continue;

            if (component.Enabled is false || !pointLight.Enabled)
                continue;

            var worldPos = _transformSystem.GetWorldPosition(xform, xformQuery);
            if (!bounds.Contains(worldPos))
                continue;

            var color = component.Color ?? pointLight.Color;
            var (_, _, worldMatrix) = _transformSystem.GetWorldPositionRotationMatrix(xform, xformQuery);
            handle.SetTransform(worldMatrix);

            var mask = _spriteSystem.Frame0(component.Sprite);
            var xOffset = component.OffsetX - mask.Width / 2f / EyeManager.PixelsPerMeter;
            var yOffset = component.OffsetY - mask.Height / 2f / EyeManager.PixelsPerMeter;
            var textureVector = new Vector2(xOffset, yOffset);

            handle.DrawTexture(mask, textureVector, color);
        }

        handle.UseShader(null);
        handle.SetTransform(Matrix3x2.Identity);
    }

    protected override void DisposeBehavior()
    {
        _cfg.UnsubValueChanged(OrionCCVars.EnableLightsGlowing, _lightCvarChanged);
        base.DisposeBehavior();
    }
}
