using Content.Shared.CCVar;
using Content.Shared.CombatMode;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Utility;

namespace Content.Orion.Client.CombatMode.Systems;

public sealed partial class CombatModeIndicatorSystem : EntitySystem
{
    [Dependency] private IConfigurationManager _cfg = default!;
    [Dependency] private IOverlayManager _overlay = default!;

    public override void Initialize()
    {
        base.Initialize();

        Subs.CVar(_cfg, CCVars.CombatIndicator, OnIndicatorSettingChanged, true);
    }

    public override void Shutdown()
    {
        _overlay.RemoveOverlay<CombatModeIndicatorOverlay>();

        base.Shutdown();
    }

    private void OnIndicatorSettingChanged(bool enabled)
    {
        if (enabled)
        {
            if (!_overlay.HasOverlay<CombatModeIndicatorOverlay>())
                _overlay.AddOverlay(new CombatModeIndicatorOverlay(EntityManager));

            return;
        }

        _overlay.RemoveOverlay<CombatModeIndicatorOverlay>();
    }
}
