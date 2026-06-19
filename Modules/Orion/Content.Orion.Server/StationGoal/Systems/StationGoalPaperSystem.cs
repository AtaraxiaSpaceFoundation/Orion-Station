// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Collections.Generic;
using Content.Orion.Shared.CCVar;
using Content.Orion.Shared.StationGoal;
using Content.Orion.Shared.StationGoal.Components;
using Content.Server.Fax;
using Content.Server.GameTicking;
using Content.Server.Station.Systems;
using Content.Shared.Fax.Components;
using Robust.Server.Player;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Maths;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;

namespace Content.Orion.Server.StationGoal.Systems;

/// <summary>
///     System to spawn paper with station goal.
/// </summary>
public sealed partial class StationGoalPaperSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _proto = default!;
    [Dependency] private IRobustRandom _random = default!;
    [Dependency] private FaxSystem _fax = default!;
    [Dependency] private IPlayerManager _playerManager = default!;
    [Dependency] private StationSystem _station = default!;
    [Dependency] private IConfigurationManager _cfg = default!;

    public override void Initialize()
    {
        SubscribeLocalEvent<GameRunLevelChangedEvent>(OnRunLevelChanged);
    }

    private void OnRunLevelChanged(GameRunLevelChangedEvent ev)
    {
        if (ev.New != GameRunLevel.InRound || !_cfg.GetCVar(OrionCCVars.StationGoal))
            return;

        var playerCount = _playerManager.PlayerCount;

        foreach (var uid in _station.GetStations())
        {
            var tempGoals = GetStationGoals(uid);
            StationGoalPrototype? selGoal = null;
            while (tempGoals.Count > 0)
            {
                var goalId = _random.Pick(tempGoals);
                var goalProto = _proto.Index(goalId);

                if (goalProto.MaxPlayers is { } maxPlayers && playerCount > maxPlayers || goalProto.MinPlayers is { } minPlayers && playerCount < minPlayers)
                {
                    tempGoals.Remove(goalId);
                    continue;
                }

                selGoal = goalProto;
                break;
            }

            if (selGoal is null)
                continue;

            if (SendStationGoal(uid, selGoal))
                Log.Info($"Goal {selGoal.ID} has been sent to station {MetaData(uid).EntityName}");
        }
    }

    private List<ProtoId<StationGoalPrototype>> GetStationGoals(EntityUid station)
    {
        return TryComp<StationGoalComponent>(station, out var stationGoal)
            ? new List<ProtoId<StationGoalPrototype>>(stationGoal.Goals)
            : new List<ProtoId<StationGoalPrototype>>();
    }

    public bool SendStationGoal(EntityUid ent, ProtoId<StationGoalPrototype> goal)
    {
        return SendStationGoal(ent, _proto.Index(goal));
    }

    /// <summary>
    ///     Send a station goal on selected station to all faxes which are authorized to receive it.
    /// </summary>
    /// <returns>True if at least one fax received paper</returns>
    private bool SendStationGoal(EntityUid ent, StationGoalPrototype goal)
    {
        var printout = new FaxPrintout(
            Loc.GetString(goal.Text, ("station", MetaData(ent).EntityName)),
            Loc.GetString("station-goal-fax-paper-name"),
            null,
            null,
            "paper_stamp-centcom",
            [new() { StampedName = Loc.GetString("stamp-component-stamped-name-centcom"), StampedColor = Color.FromHex("#006600") }]
        );

        var wasSent = false;
        var query = EntityQueryEnumerator<FaxMachineComponent>();
        while (query.MoveNext(out var faxUid, out var fax))
        {
            if (!TryComp<StationGoalFaxComponent>(faxUid, out var receiver))
                continue;

            if (!receiver.ReceiveAllStationGoals && !(receiver.ReceiveStationGoal && _station.GetOwningStation(faxUid) == ent))
                continue;

            _fax.Receive(faxUid, printout, null, fax);
            wasSent = true;

            foreach (var spawnEnt in goal.Spawns)
            {
                SpawnAtPosition(spawnEnt, Transform(faxUid).Coordinates);
            }

        }

        return wasSent;
    }
}
