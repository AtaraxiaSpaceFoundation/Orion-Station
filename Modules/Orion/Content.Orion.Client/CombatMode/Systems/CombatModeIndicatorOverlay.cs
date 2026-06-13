using System.Numerics;
using Content.Shared.CombatMode;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Enums;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Maths;
using Robust.Shared.Timing;
using Robust.Shared.Utility;

namespace Content.Orion.Client.CombatMode.Systems;

public sealed class CombatModeIndicatorOverlay : Overlay
{
    private readonly IEntityManager _entity;
    private readonly IGameTiming _timing;
    private readonly SpriteSystem _sprite;
    private readonly SharedTransformSystem _transform;

    private static readonly SpriteSpecifier IndicatorSprite =
        new SpriteSpecifier.Rsi(new ResPath("/Textures/_Orion/Effects/combat_mode.rsi"), "combat_mode");

    public override OverlaySpace Space => OverlaySpace.WorldSpaceBelowFOV;

    public CombatModeIndicatorOverlay(IEntityManager entity)
    {
        _entity = entity;
        _timing = IoCManager.Resolve<IGameTiming>();
        _sprite = entity.System<SpriteSystem>();
        _transform = entity.System<SharedTransformSystem>();
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        var handle = args.WorldHandle;
        var eyeRotation = args.Viewport.Eye?.Rotation ?? Angle.Zero;
        var rotationMatrix = Matrix3Helpers.CreateRotation(-eyeRotation);
        var xformQuery = _entity.GetEntityQuery<TransformComponent>();
        var texture = _sprite.GetFrame(IndicatorSprite, _timing.RealTime);
        var position = -(Vector2) texture.Size / 2f / EyeManager.PixelsPerMeter;

        var query = _entity.EntityQueryEnumerator<CombatModeComponent, SpriteComponent, TransformComponent>();
        while (query.MoveNext(out var uid, out var combatMode, out var sprite, out var xform))
        {
            if (!combatMode.IsInCombatMode || !sprite.Visible || xform.MapID != args.MapId)
                continue;

            var worldPosition = _transform.GetWorldPosition(xform, xformQuery);
            if (!_sprite.GetLocalBounds((uid, sprite)).Translated(worldPosition).Intersects(args.WorldAABB))
                continue;

            var worldMatrix = Matrix3Helpers.CreateTranslation(worldPosition);
            handle.SetTransform(Matrix3x2.Multiply(rotationMatrix, worldMatrix));
            handle.DrawTexture(texture, position);
        }

        handle.SetTransform(Matrix3x2.Identity);
    }
}
