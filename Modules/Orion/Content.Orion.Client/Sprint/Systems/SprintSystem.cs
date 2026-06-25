// SPDX-FileCopyrightText: 2026 ReWAFFlution <rockstarfly65@gmail.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Orion.Shared.Sprint;
using Content.Shared.Input;
using Robust.Client.Input;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Spawners;
using Robust.Shared.Timing;

namespace Content.Orion.Client.Sprint.Systems;

/// <summary>
/// Client-side sprint system.
/// Registers the Space key binding and handles VFX sprite spawning during sprint.
/// </summary>
public sealed partial class ClientSprintSystem : EntitySystem
{
    [Dependency] private IInputManager _inputManager = default!;
    [Dependency] private IGameTiming _timing = default!;

    private EntityQuery<SprintComponent> _sprintQuery;
    private EntityQuery<SprintSpriteComponent> _spriteQuery;

    public override void Initialize()
    {
        base.Initialize();

        _sprintQuery = GetEntityQuery<SprintComponent>();
        _spriteQuery = GetEntityQuery<SprintSpriteComponent>();

        // Register Space key binding for Sprint (State type: hold to sprint, release to stop)
        _inputManager.RegisterBinding(new KeyBindingRegistration
        {
            Function = ContentKeyFunctions.Sprint,
            BaseKey = Keyboard.Key.Space,
            Type = KeyBindingType.State
        });
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        // Client-only VFX: only spawn on the first prediction tick, not during replay or on server
        if (!_timing.IsFirstTimePredicted)
            return;

        var query = EntityQueryEnumerator<SprintComponent, SprintSpriteComponent>();
        while (query.MoveNext(out var uid, out var sprint, out var sprite))
        {
            if (!sprint.IsSprinting)
                continue;

            sprite.TimeSinceLastSpawn += frameTime;
            if (sprite.TimeSinceLastSpawn < sprite.SpawnInterval)
                continue;

            // Spawn the VFX entity at the player's feet with auto-despawn
            var xform = Transform(uid);
            var ent = Spawn(sprite.EntProtoId, xform.Coordinates);
            var despawn = EnsureComp<TimedDespawnComponent>(ent);
            despawn.Lifetime = 0.3f;

            sprite.TimeSinceLastSpawn = 0;
        }
    }
}
