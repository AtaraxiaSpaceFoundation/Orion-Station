// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Server.Ghost;
using Content.Orion.Shared.CustomGhost;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;

namespace Content.Orion.Server.Ghost;

public sealed partial class CustomGhostSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _prototype = default!;
    [Dependency] private ISharedPlayerManager _player = default!;

    private static readonly ProtoId<CustomGhostPrototype> DefaultCustomGhostPrototype = "default";

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<ResolveCustomGhostPrototypeEvent>(OnResolveCustomGhostPrototype);
    }

    private void OnResolveCustomGhostPrototype(ResolveCustomGhostPrototypeEvent args)
    {
        if (!_player.TryGetSessionById(args.UserId, out var session))
            return;

        if (!_prototype.TryIndex<CustomGhostPrototype>(args.CustomGhostId, out var customGhost) || customGhost.Abstract || !customGhost.CanUse(session))
            customGhost = _prototype.Index(DefaultCustomGhostPrototype);

        args.GhostPrototype = customGhost.GhostEntityPrototype;
        args.SupportsDeathDamageState = customGhost.SupportsDeathDamageState;
    }
}
