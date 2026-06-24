using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;
using Content.Shared.Input;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;
using Robust.Shared.GameObjects;
using Robust.Shared.Input;
using Robust.Shared.Input.Binding;
using Robust.Shared.Log;
using Robust.Shared.Player;
using Robust.Shared.Timing;

namespace Content.Orion.Shared.Sprint.Systems;

/// <summary>
/// Handles the sprint mechanic for entities with SprintComponent.
/// Manages stamina drain, cooldown, and speed modifier application.
/// </summary>
public sealed partial class SharedSprintSystem : EntitySystem
{
    [Dependency] private IGameTiming _timing = default!;
    [Dependency] private SharedStaminaSystem _stamina = default!;
    [Dependency] private MovementSpeedModifierSystem _movementSpeed = default!;
    [Dependency] private ILogManager _logManager = default!;

    private EntityQuery<SprintComponent> _sprintQuery;
    private EntityQuery<StaminaComponent> _staminaQuery;
    private EntityQuery<MovementSpeedModifierComponent> _moveModQuery;
    private EntityQuery<InputMoverComponent> _moverQuery;

    private ISawmill _log = default!;

    public override void Initialize()
    {
        base.Initialize();

        _log = _logManager.GetSawmill("Sprint");

        _sprintQuery = GetEntityQuery<SprintComponent>();
        _staminaQuery = GetEntityQuery<StaminaComponent>();
        _moveModQuery = GetEntityQuery<MovementSpeedModifierComponent>();
        _moverQuery = GetEntityQuery<InputMoverComponent>();

        SubscribeLocalEvent<SprintComponent, ComponentShutdown>(OnSprintShutdown);
        SubscribeLocalEvent<SprintComponent, RefreshMovementSpeedModifiersEvent>(OnRefreshMovementSpeed);
        SubscribeLocalEvent<SprintComponent, MapInitEvent>(OnSprintMapInit);

        // Register input handler via CommandBinds (works on both client and server)
        CommandBinds.Builder
            .Bind(ContentKeyFunctions.Sprint, new SprintInputCmdHandler(this))
            .Register<SharedSprintSystem>();
    }

    public override void Update(float frameTime)
    {
        base.Update(frameTime);

        var query = EntityQueryEnumerator<SprintComponent>();
        while (query.MoveNext(out var uid, out var sprint))
        {
            UpdateCooldown(uid, sprint, frameTime);
            UpdateSprint(uid, sprint, frameTime);
        }
    }

    private void OnSprintMapInit(Entity<SprintComponent> ent, ref MapInitEvent args)
    {
        // _log.Debug($"SprintComponent initialized on entity {ToPrettyString(ent)}");
    }

    private void UpdateSprint(EntityUid uid, SprintComponent sprint, float frameTime)
    {
        if (!sprint.IsSprinting)
            return;

        // Stop sprint if entity can no longer move (stunned, dead, asleep, etc.)
        if (!CanSprintEntity(uid))
        {
            // _log.Info($"Stopping sprint on {ToPrettyString(uid)} - entity cannot move");
            StopSprint(uid, sprint);
            return;
        }

        if (!_staminaQuery.TryGetComponent(uid, out var stamina))
        {
            // _log.Warning($"Stopping sprint on {ToPrettyString(uid)} - missing StaminaComponent");
            StopSprint(uid, sprint);
            return;
        }

        // Check if stamina would be depleted by this tick's drain
        var drainAmount = sprint.StaminaDrainRate * frameTime;
        var currentStamina = stamina.CritThreshold - stamina.StaminaDamage;
        if (currentStamina - drainAmount <= 0)
        {
            // _log.Info($"Stopping sprint on {ToPrettyString(uid)} - stamina depleted");
            StopSprint(uid, sprint);
            return;
        }

        // Drain stamina without visual effects and without admin log spam
        _stamina.TakeStaminaDamage(uid, drainAmount, stamina, visual: false, log: false);
    }

    private void UpdateCooldown(EntityUid uid, SprintComponent sprint, float frameTime)
    {
        if (!sprint.IsOnCooldown)
            return;

        sprint.CooldownRemaining -= frameTime;
        if (sprint.CooldownRemaining <= 0)
        {
            sprint.IsOnCooldown = false;
            sprint.CooldownRemaining = 0;
            Dirty(uid, sprint);
            // _log.Info($"Cooldown ended for {ToPrettyString(uid)}");
        }
    }

    private void OnSprintShutdown(Entity<SprintComponent> ent, ref ComponentShutdown args)
    {
        if (ent.Comp.IsSprinting)
        {
            StopSprint(ent.Owner, ent.Comp);
        }
    }

    private void OnRefreshMovementSpeed(Entity<SprintComponent> ent, ref RefreshMovementSpeedModifiersEvent args)
    {
        if (!ent.Comp.IsSprinting)
            return;

        // Apply sprint multiplier only to sprint speed
        args.ModifySpeed(1.0f, ent.Comp.SprintMultiplier);
    }

    /// <summary>
    /// Attempts to start sprinting. Returns true if successful.
    /// </summary>
    public bool TryStartSprint(EntityUid uid, SprintComponent? sprint = null, StaminaComponent? stamina = null)
    {
        // Auto-add SprintComponent if the entity has stamina but no sprint component yet.
        if (!_sprintQuery.Resolve(uid, ref sprint, false))
        {
            if (!_staminaQuery.HasComponent(uid))
                return false;

            sprint = EnsureComp<SprintComponent>(uid);
            // _log.Debug($"Auto-added SprintComponent to {ToPrettyString(uid)}");
        }

        if (sprint.IsSprinting || // Check if already sprinting
            sprint.IsOnCooldown || // Check cooldown
            !CanSprintEntity(uid) || // Check if entity can move (handles stunned, dead, etc.)
            !_staminaQuery.Resolve(uid, ref stamina, false)) //Check stamina
            return false;

        var currentStamina = stamina.CritThreshold - stamina.StaminaDamage;
        if (currentStamina < sprint.MinStaminaToSprint)
            return false;

        // Apply sprint
        sprint.IsSprinting = true;
        Dirty(uid, sprint);

        // Refresh movement speed to apply the sprint modifier
        _movementSpeed.RefreshMovementSpeedModifiers(uid);

        // _log.Info($"Sprint started on {ToPrettyString(uid)}");
        return true;
    }

    /// <summary>
    /// Stops sprinting and initiates cooldown.
    /// </summary>
    public void StopSprint(EntityUid uid, SprintComponent? sprint = null)
    {
        if (!_sprintQuery.Resolve(uid, ref sprint, false) ||
            !sprint.IsSprinting)
            return;

        sprint.IsSprinting = false;
        sprint.IsOnCooldown = true;
        sprint.CooldownRemaining = sprint.CooldownTime;
        Dirty(uid, sprint);

        // Refresh movement speed to remove the sprint modifier
        _movementSpeed.RefreshMovementSpeedModifiers(uid);

        // _log.Info($"Sprint stopped on {ToPrettyString(uid)}, cooldown {sprint.CooldownTime}s started");
    }

    private bool CanSprintEntity(EntityUid uid)
    {
        if (EntityManager.IsQueuedForDeletion(uid))
            return false;

        // Check CanMove flag (handles stun, sleep, death, etc.)
        if (_moverQuery.TryGetComponent(uid, out var mover) && !mover.CanMove)
            return false;

        return true;
    }

    /// <summary>
    /// Handles sprint input via CommandBinds. Calls TryStartSprint on key down
    /// and StopSprint on key up.
    /// </summary>
    private sealed class SprintInputCmdHandler : InputCmdHandler
    {
        private readonly SharedSprintSystem _system;

        public SprintInputCmdHandler(SharedSprintSystem system)
        {
            _system = system;
        }

        public override bool HandleCmdMessage(IEntityManager entManager, ICommonSession? session, IFullInputCmdMessage message)
        {
            if (session?.AttachedEntity == null)
                return false;

            if (message.State == BoundKeyState.Down)
                _system.TryStartSprint(session.AttachedEntity.Value);
            else
                _system.StopSprint(session.AttachedEntity.Value);

            return false;
        }
    }
}
