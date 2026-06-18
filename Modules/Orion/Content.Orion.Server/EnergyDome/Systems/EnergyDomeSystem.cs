// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 TheShuEd <96445749+theshued@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using Content.Orion.Server.EnergyDome.Components;
using Content.Server.DeviceLinking.Systems;
using Content.Server.Power.EntitySystems;
using Content.Shared.Actions;
using Content.Shared.Damage.Systems;
using Content.Shared.DeviceLinking.Events;
using Content.Shared.Examine;
using Content.Shared.Interaction;
using Content.Shared.Popups;
using Content.Shared.Power;
using Content.Shared.Power.Components;
using Content.Shared.PowerCell;
using Content.Shared.PowerCell.Components;
using Content.Shared.Timing;
using Content.Shared.Toggleable;
using Content.Shared.Verbs;
using Robust.Shared.Audio.Systems;
using Robust.Shared.Containers;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Orion.Server.EnergyDome.Systems;

public sealed partial class EnergyDomeSystem : EntitySystem
{
    [Dependency] private SharedAudioSystem _audio = default!;
    [Dependency] private BatterySystem _battery = default!;
    [Dependency] private SharedContainerSystem _container = default!;
    [Dependency] private UseDelaySystem _useDelay = default!;
    [Dependency] private SharedTransformSystem _transform = default!;
    [Dependency] private SharedPopupSystem _popup = default!;
    [Dependency] private PowerCellSystem _powerCell = default!;
    [Dependency] private DeviceLinkSystem _signalSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, MapInitEvent>(OnInit);

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, ActivateInWorldEvent>(OnActivatedInWorld);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, AfterInteractEvent>(OnAfterInteract);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, SignalReceivedEvent>(OnSignalReceived);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, GetItemActionsEvent>(OnGetActions);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, ToggleActionEvent>(OnToggleAction);

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, PowerCellChangedEvent>(OnPowerCellChanged);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, PowerCellSlotEmptyEvent>(OnPowerCellSlotEmpty);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, ChargeChangedEvent>(OnChargeChanged);

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, EntParentChangedMessage>(OnParentChanged);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, EntInsertedIntoContainerMessage>(OnContainerChanged);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, EntRemovedFromContainerMessage>(OnContainerChanged);

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, GetVerbsEvent<ActivationVerb>>(AddToggleDomeVerb);
        SubscribeLocalEvent<EnergyDomeGeneratorComponent, ExaminedEvent>(OnExamine);

        SubscribeLocalEvent<EnergyDomeGeneratorComponent, ComponentRemove>(OnComponentRemove);

        SubscribeLocalEvent<EnergyDomeComponent, DamageDealtEvent>(OnDomeDamaged);
    }

    private void OnInit(Entity<EnergyDomeGeneratorComponent> generator, ref MapInitEvent args)
    {
        if (generator.Comp.CanDeviceNetworkUse)
            _signalSystem.EnsureSinkPorts(generator, generator.Comp.TogglePort, generator.Comp.OnPort, generator.Comp.OffPort);
    }

    #region Use Ways

    private void OnSignalReceived(Entity<EnergyDomeGeneratorComponent> generator, ref SignalReceivedEvent args)
    {
        if (!generator.Comp.CanDeviceNetworkUse)
            return;

        if (args.Port == generator.Comp.OnPort)
            AttemptToggle(generator, true);

        if (args.Port == generator.Comp.OffPort)
            AttemptToggle(generator, false);

        if (args.Port == generator.Comp.TogglePort)
            AttemptToggle(generator, !generator.Comp.Enabled);
    }

    private void OnAfterInteract(Entity<EnergyDomeGeneratorComponent> generator, ref AfterInteractEvent args)
    {
        if (generator.Comp.CanInteractUse)
            AttemptToggle(generator, !generator.Comp.Enabled);
    }

    private void OnActivatedInWorld(Entity<EnergyDomeGeneratorComponent> generator, ref ActivateInWorldEvent args)
    {
        if (generator.Comp.CanInteractUse)
            AttemptToggle(generator, !generator.Comp.Enabled);
    }

    private void OnExamine(Entity<EnergyDomeGeneratorComponent> generator, ref ExaminedEvent args)
    {
        args.PushMarkup(Loc.GetString(
            (generator.Comp.Enabled)
            ? "energy-dome-on-examine-is-on-message"
            : "energy-dome-on-examine-is-off-message"
            ));
    }

    private void AddToggleDomeVerb(Entity<EnergyDomeGeneratorComponent> generator, ref GetVerbsEvent<ActivationVerb> args)
    {
        if (!args.CanAccess || !args.CanInteract || !generator.Comp.CanInteractUse)
            return;

        ActivationVerb verb = new()
        {
            Text = Loc.GetString("energy-dome-verb-toggle"),
            Act = () => AttemptToggle(generator, !generator.Comp.Enabled)
        };

        args.Verbs.Add(verb);
    }

    private static void OnGetActions(Entity<EnergyDomeGeneratorComponent> generator, ref GetItemActionsEvent args)
    {
        if (generator.Comp.CanInteractUse)
            args.AddAction(ref generator.Comp.ToggleActionEntity, generator.Comp.ToggleAction);
    }

    private void OnToggleAction(Entity<EnergyDomeGeneratorComponent> generator, ref ToggleActionEvent args)
    {
        if (args.Handled)
            return;

        AttemptToggle(generator, !generator.Comp.Enabled);
        args.Handled = true;
    }

    #endregion

    #region Interactions

    private void OnPowerCellSlotEmpty(Entity<EnergyDomeGeneratorComponent> generator, ref PowerCellSlotEmptyEvent args)
    {
        TurnOff(generator, true);
    }

    private void OnPowerCellChanged(Entity<EnergyDomeGeneratorComponent> generator, ref PowerCellChangedEvent args)
    {
        if (HasComp<PowerCellDrawComponent>(generator) && (args.Ejected || !HasPowerCellCharge(generator)))
            TurnOff(generator, true);
    }

    private void OnChargeChanged(Entity<EnergyDomeGeneratorComponent> generator, ref ChargeChangedEvent args)
    {
        if (!HasComp<PowerCellDrawComponent>(generator) && args.CurrentCharge == 0)
            TurnOff(generator, true);
    }

    private void OnDomeDamaged(Entity<EnergyDomeComponent> dome, ref DamageDealtEvent args)
    {
        if (dome.Comp.Generator == null)
            return;

        var generatorUid = dome.Comp.Generator.Value;
        if (!TryComp<EnergyDomeGeneratorComponent>(generatorUid, out var generatorComp))
            return;

        var totalDamage = args.Damage.GetTotal().Float();
        var energyLeak = totalDamage * generatorComp.DamageEnergyDraw;

        _audio.PlayPvs(generatorComp.ParrySound, dome);

        if (HasComp<PowerCellDrawComponent>(generatorUid))
        {
            if (TryComp<PowerCellSlotComponent>(generatorUid, out var slot) && _powerCell.TryGetBatteryFromSlot((generatorUid, slot), out var cell))
            {
                var batteryEntity = cell.Value.AsNullable();
                _battery.UseCharge(batteryEntity, energyLeak);

                if (_battery.GetCharge(batteryEntity) == 0)
                    TurnOff((generatorUid, generatorComp), true);
            }

            return;
        }

        // It seems to me it would not work well to hang both a powercell and an internal battery with wire charging on the object....
        if (!TryComp<BatteryComponent>(generatorUid, out var battery))
            return;

        _battery.UseCharge((generatorUid, battery), energyLeak);

        if (_battery.GetCharge((generatorUid, battery)) == 0)
            TurnOff((generatorUid, generatorComp), true);
    }

    private void OnParentChanged(Entity<EnergyDomeGeneratorComponent> generator, ref EntParentChangedMessage args)
    {
        if (GetProtectedEntity(generator) != generator.Comp.DomeParentEntity)
            TurnOff(generator, false);
    }

    private void OnContainerChanged(Entity<EnergyDomeGeneratorComponent> generator, ref EntInsertedIntoContainerMessage args)
    {
        TurnOff(generator, false);
    }

    private void OnContainerChanged(Entity<EnergyDomeGeneratorComponent> generator, ref EntRemovedFromContainerMessage args)
    {
        TurnOff(generator, false);
    }

    private void OnComponentRemove(Entity<EnergyDomeGeneratorComponent> generator, ref ComponentRemove args)
    {
        TurnOff(generator, false);
    }

    #endregion

    #region Functional

    private void AttemptToggle(Entity<EnergyDomeGeneratorComponent> generator, bool status)
    {
        if (_useDelay.IsDelayed(generator.Owner))
        {
            _audio.PlayPvs(generator.Comp.TurnOffSound, generator);
            _popup.PopupEntity(Loc.GetString("energy-dome-recharging"), generator);
            return;
        }

        if (HasComp<PowerCellDrawComponent>(generator))
        {
            if (!HasPowerCellCharge(generator))
            {
                _audio.PlayPvs(generator.Comp.TurnOffSound, generator);
                _popup.PopupEntity(Loc.GetString("energy-dome-no-cell"), generator);
                return;
            }
        }
        else if (TryComp<BatteryComponent>(generator, out var battery))
        {
            if (_battery.GetCharge((generator, battery)) == 0)
            {
                _audio.PlayPvs(generator.Comp.TurnOffSound, generator);
                _popup.PopupEntity(Loc.GetString("energy-dome-no-power"), generator);
                return;
            }
        }

        Toggle(generator, status);
    }

    private void Toggle(Entity<EnergyDomeGeneratorComponent> generator, bool status)
    {
        if (status)
            TurnOn(generator);
        else
            TurnOff(generator, false);
    }

    private void TurnOn(Entity<EnergyDomeGeneratorComponent> generator)
    {
        if (generator.Comp.Enabled)
            return;

        var protectedEntity = GetProtectedEntity(generator);

        var newDome = Spawn(generator.Comp.DomePrototype, Transform(protectedEntity).Coordinates);
        generator.Comp.DomeParentEntity = protectedEntity;
        _transform.SetParent(newDome, protectedEntity);

        if (TryComp<EnergyDomeComponent>(newDome, out var domeComp))
            domeComp.Generator = generator;

        if (TryComp<PowerCellDrawComponent>(generator.Owner, out _))
            _powerCell.SetDrawEnabled(generator.Owner, true);

        if (TryComp<BatterySelfRechargerComponent>(generator, out var recharger))
            SetSelfRechargeEnabled((generator, recharger), true);

        generator.Comp.SpawnedDome = newDome;
        _audio.PlayPvs(generator.Comp.TurnOnSound, generator);
        generator.Comp.Enabled = true;
    }

    private void TurnOff(Entity<EnergyDomeGeneratorComponent> generator, bool startReloading)
    {
        if (!generator.Comp.Enabled)
            return;

        generator.Comp.Enabled = false;
        QueueDel(generator.Comp.SpawnedDome);

        if (TryComp<PowerCellDrawComponent>(generator.Owner, out _))
            _powerCell.SetDrawEnabled(generator.Owner, false);

        if (TryComp<BatterySelfRechargerComponent>(generator, out var recharger))
            SetSelfRechargeEnabled((generator, recharger), false);

        _audio.PlayPvs(generator.Comp.TurnOffSound, generator);

        if (!startReloading)
            return;

        _audio.PlayPvs(generator.Comp.EnergyOutSound, generator);

        if (TryComp<UseDelayComponent>(generator, out var useDelay))
            _useDelay.TryResetDelay(new Entity<UseDelayComponent>(generator, useDelay));
    }

    #endregion

    #region Util

    private EntityUid GetProtectedEntity(EntityUid entity)
    {
        return (_container.TryGetOuterContainer(entity, Transform(entity), out var container))
            ? container.Owner
            : entity;
    }

    private bool HasPowerCellCharge(EntityUid generator)
    {
        return TryComp<PowerCellDrawComponent>(generator, out var draw) &&
               TryComp<PowerCellSlotComponent>(generator, out var slot) &&
               _powerCell.TryGetBatteryFromSlot((generator, slot), out _) &&
               _powerCell.HasDrawCharge((generator, draw, slot));
    }

    private void SetSelfRechargeEnabled(Entity<BatterySelfRechargerComponent> recharger, bool enabled)
    {
        recharger.Comp.NextAutoRecharge = enabled
            ? null
            : TimeSpan.MaxValue;
        Dirty(recharger);
        _battery.RefreshChargeRate(recharger.Owner);
    }

    #endregion
}
