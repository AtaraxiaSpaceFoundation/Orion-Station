using Content.Shared.Actions;
using Content.Shared.Bed.Sleep;
using Content.Shared.Mind;
using Content.Shared.Mobs.Systems;
using Content.Shared.MouseRotator;
using Content.Shared.Movement.Components;
using Content.Shared.NPC.Systems;
using Content.Shared.Popups;
using Robust.Shared.Timing;

namespace Content.Shared.CombatMode;

public abstract partial class SharedCombatModeSystem : EntitySystem
{
    [Dependency] protected IGameTiming Timing = default!;
    [Dependency] private SharedActionsSystem _actionsSystem = default!;
    [Dependency] private SharedPopupSystem _popup = default!;
    [Dependency] private SharedMindSystem _mind = default!;
    [Dependency] private SharedNPCSystem _npc = default!;
    // Orion-Start
    [Dependency] private MobStateSystem _mobState = default!;
    // Orion-End

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CombatModeComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<CombatModeComponent, ComponentShutdown>(OnShutdown);
        SubscribeLocalEvent<CombatModeComponent, ToggleCombatActionEvent>(OnActionPerform);
    }

    private void OnMapInit(EntityUid uid, CombatModeComponent component, MapInitEvent args)
    {
        _actionsSystem.AddAction(uid, ref component.CombatToggleActionEntity, component.CombatToggleAction);
        Dirty(uid, component);
    }

    private void OnShutdown(EntityUid uid, CombatModeComponent component, ComponentShutdown args)
    {
        _actionsSystem.RemoveAction(uid, component.CombatToggleActionEntity);

        // Orion-Start
        RaiseLocalEvent(uid, new CombatModeChangedEvent(false));
        // Orion-End

        SetMouseRotatorComponents(uid, false);
    }

    private void OnActionPerform(EntityUid uid, CombatModeComponent component, ToggleCombatActionEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;
        var requestedCombatMode = !component.IsInCombatMode; // Orion
        SetInCombatMode(uid, !component.IsInCombatMode, component);

        // Orion-Edit-Start
        var msg = component.IsInCombatMode != requestedCombatMode
            ? "action-popup-combat-enabled"
            : "action-popup-combat-disabled";
        // Orion-Edit-End

        // Orion-Start
        if (!ShouldShowCombatModePopup())
            return;
        // Orion-End

        _popup.PopupClient(Loc.GetString(msg), args.Performer, args.Performer);
    }

    public void SetCanDisarm(EntityUid entity, bool canDisarm, CombatModeComponent? component = null)
    {
        if (!Resolve(entity, ref component))
            return;

        component.CanDisarm = canDisarm;
    }

    public bool IsInCombatMode(EntityUid? entity, CombatModeComponent? component = null)
    {
        return entity != null && Resolve(entity.Value, ref component, false) && component.IsInCombatMode;
    }

    public virtual void SetInCombatMode(EntityUid entity, bool value, CombatModeComponent? component = null)
    {
        if (!Resolve(entity, ref component))
            return;

        if (component.IsInCombatMode == value)
            return;

        // Orion-Start
        if (value && (_mobState.IsIncapacitated(entity) || HasComp<SleepingComponent>(entity)))
            return;
        // Orion-End

        component.IsInCombatMode = value;
        Dirty(entity, component);

        // Orion-Start
        RaiseLocalEvent(entity, new CombatModeChangedEvent(value));
        // Orion-End

        if (component.CombatToggleActionEntity != null)
            _actionsSystem.SetToggled(component.CombatToggleActionEntity, component.IsInCombatMode);

        // Change mouse rotator comps if flag is set
        if (!component.ToggleMouseRotator || _npc.IsNpc(entity) && !_mind.TryGetMind(entity, out _, out _))
            return;

        SetMouseRotatorComponents(entity, value);
    }

    private void SetMouseRotatorComponents(EntityUid uid, bool value)
    {
        if (value)
        {
            EnsureComp<MouseRotatorComponent>(uid);
            EnsureComp<NoRotateOnMoveComponent>(uid);
        }
        else
        {
            RemComp<MouseRotatorComponent>(uid);
            RemComp<NoRotateOnMoveComponent>(uid);
        }
    }

    // Orion-Start
    protected virtual bool ShouldShowCombatModePopup()
    {
        return true;
    }
    // Orion-End
}

public sealed partial class ToggleCombatActionEvent : InstantActionEvent
{

}
