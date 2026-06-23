// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 ThereDrD <88589686+theredrd0@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Client.Graphics;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Prototypes;

namespace Content.Orion.Client.Lighting.Shaders;

public sealed partial class LightingOverlaySystem : EntitySystem
{
    [Dependency] private IOverlayManager _overlayManager = default!;
    [Dependency] private IPrototypeManager _prototypeManager = default!;

    private LightingOverlay? _lightingOverlay;

    public override void Initialize()
    {
        base.Initialize();

        _lightingOverlay = new LightingOverlay(EntityManager, _prototypeManager);
        _overlayManager.AddOverlay(_lightingOverlay);
    }

    public override void Shutdown()
    {
        base.Shutdown();

        if (_lightingOverlay == null)
            return;

        _overlayManager.RemoveOverlay(_lightingOverlay);
        _lightingOverlay.Dispose();
        _lightingOverlay = null;
    }
}
