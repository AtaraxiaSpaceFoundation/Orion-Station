using Content.Shared.CCVar;
using Content.Shared.CombatMode;
using Robust.Client.GameObjects;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Utility;

namespace Content.Orion.Client.CombatMode;

public sealed partial class CombatModeIndicatorSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private SpriteSystem _sprite = default!;
    [Dependency] private EntityQuery<SpriteComponent> _spriteQuery;

    private const string IndicatorLayer = "combat_mode_indicator";
    private static readonly SpriteSpecifier IndicatorSprite = new SpriteSpecifier.Rsi(new ResPath("/Textures/_Orion/Effects/combat_mode.rsi"), "combat_mode");

    private bool _enabled;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CombatModeComponent, CombatModeChangedEvent>(OnCombatModeChanged);

        Subs.CVar(_cfg, CCVars.CombatIndicator, OnIndicatorSettingChanged, true);
    }

    private void OnCombatModeChanged(Entity<CombatModeComponent> entity, ref CombatModeChangedEvent args)
    {
        UpdateIndicator(entity.Owner, args.InCombatMode);
    }

    private void OnIndicatorSettingChanged(bool enabled)
    {
        _enabled = enabled;

        var query = EntityQueryEnumerator<CombatModeComponent>();
        while (query.MoveNext(out var uid, out var component))
        {
            UpdateIndicator((uid, component));
        }
    }

    private void UpdateIndicator(Entity<CombatModeComponent> entity)
    {
        UpdateIndicator(entity.Owner, entity.Comp.IsInCombatMode);
    }

    private void UpdateIndicator(EntityUid uid, bool inCombatMode)
    {
        if (!_enabled || !inCombatMode)
        {
            RemoveIndicator(uid);
            return;
        }

        if (!_spriteQuery.TryComp(uid, out var sprite) || _sprite.LayerMapTryGet((uid, sprite), IndicatorLayer, out _, false))
            return;

        var layer = _sprite.AddLayer((uid, sprite), IndicatorSprite);
        _sprite.LayerMapSet((uid, sprite), IndicatorLayer, layer);
    }

    private void RemoveIndicator(EntityUid uid)
    {
        if (_spriteQuery.TryComp(uid, out var sprite) && _sprite.LayerMapTryGet((uid, sprite), IndicatorLayer, out var layer, false))
            _sprite.RemoveLayer((uid, sprite), layer);
    }
}
