using System;
using Content.Orion.Shared.CCVar;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Timing;

namespace Content.Orion.Shared.Ghost;

public abstract partial class SharedGhostReturnToRoundSystem : EntitySystem
{
    [Dependency] protected IConfigurationManager Cfg = default!;
    [Dependency] protected IGameTiming GameTiming = default!;

    public override void Initialize()
    {
        base.Initialize();

        Cfg.OnValueChanged(OrionCCVars.GhostRespawnTime,
            ghostRespawnTime =>
            {
                GhostRespawnTime = TimeSpan.FromSeconds(ghostRespawnTime);
            },
            true);
    }

    protected TimeSpan GhostRespawnTime = new(0, 5, 0);
}
