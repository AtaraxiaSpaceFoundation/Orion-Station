// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Linq;
using Content.Server.Administration;
using Content.Server.Commands;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.Prototypes;
using Content.Orion.Shared.StationGoal;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Localization;

namespace Content.Orion.Server.StationGoal;

[AdminCommand(AdminFlags.Fun)]
public sealed partial class StationGoalCommand : IConsoleCommand
{
    [Dependency] private IEntityManager _entManager = default!;
    [Dependency] private IPrototypeManager _prototypeManager = default!;

    public string Command => "sendstationgoal";
    public string Description => Loc.GetString("send-station-goal-command-description");
    public string Help => Loc.GetString("send-station-goal-command-help-text", ("command", Command));

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        if (args.Length != 2)
        {
            shell.WriteError(Loc.GetString("shell-wrong-arguments-number"));
            return;
        }

        if (!NetEntity.TryParse(args[0], out var euidNet) || !_entManager.TryGetEntity(euidNet, out var euid))
        {
            shell.WriteError(Loc.GetString("send-station-goal-command-error-euid", ("euid", args[0])));
            return;
        }

        var protoId = args[1];
        if (!_prototypeManager.TryIndex<StationGoalPrototype>(protoId, out _))
        {
            shell.WriteError(Loc.GetString("send-station-goal-command-error-goal", ("protoId", protoId)));
            return;
        }

        var stationGoalPaper = _entManager.System<Systems.StationGoalPaperSystem>();
        if (!stationGoalPaper.SendStationGoal(euid.Value, protoId))
            shell.WriteError(Loc.GetString("send-station-goal-command-error-not-sent"));
    }

    public CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        switch (args.Length)
        {
            case 1:
                var stations = ContentCompletionHelper.StationIds(_entManager);
                return CompletionResult.FromHintOptions(stations, Loc.GetString("send-station-goal-command-arg-station"));
            case 2:
                var options = _prototypeManager
                    .EnumeratePrototypes<StationGoalPrototype>()
                    .Select(p => new CompletionOption(p.ID));

                return CompletionResult.FromHintOptions(options, Loc.GetString("send-station-goal-command-arg-id"));
        }
        return CompletionResult.Empty;
    }
}
