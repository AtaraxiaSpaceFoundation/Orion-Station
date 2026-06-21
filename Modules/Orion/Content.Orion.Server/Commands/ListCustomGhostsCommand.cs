// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Linq;
using System.Text;
using Content.Orion.Shared.CustomGhost;
using Content.Shared.Administration;
using Robust.Shared.Console;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Prototypes;

namespace Content.Orion.Server.Commands;

[AnyCommand]
public sealed partial class ListCustomGhostsCommand : IConsoleCommand
{
    [Dependency] private IPrototypeManager _proto = default!;

    public string Command => "listcustomghosts";
    public string Description => Loc.GetString("listcustomghosts-command-description");
    public string Help => Loc.GetString("listcustomghosts-command-help-text");

    public void Execute(IConsoleShell shell, string argStr, string[] args)
    {
        var protos = _proto.EnumeratePrototypes<CustomGhostPrototype>()
            .Where(proto => !proto.Abstract);
        var sb = new StringBuilder();
        if (shell.Player is not { } player)
        {
            foreach (var proto in protos)
            {
                sb.AppendLine(proto.ID);
            }

            shell.WriteLine(sb.ToString());
            return;
        }

        if (args.Length > 1 || args.Length == 1 && args[0] != "all")
        {
            shell.WriteLine(Help);
            return;
        }

        var showAll = args is ["all"];

        sb.AppendLine(Loc.GetString($"listcustomghosts-{(showAll ? "all" : "available")}-ghosts"));

        foreach (var proto in protos)
        {
            var visible = true;
            var available = true;

            if (proto.Restrictions is not null)
            {
                foreach (var restriction in proto.Restrictions)
                {
                    if (restriction.CanUse(player, out _))
                        continue;

                    if (restriction.HideOnFail)
                    {
                        visible = false;
                        continue;
                    }

                    available = false;
                }
            }

            if (!visible)
                continue;

            if (available)
                sb.AppendLine($"- {proto.ID}");
            else if (showAll)
                sb.AppendLine($"- {proto.ID} {Loc.GetString("listcustomghosts-locked")}");
        }

        shell.WriteLine(sb.ToString());
    }

    public CompletionResult GetCompletion(IConsoleShell shell, string[] args)
    {
        return args.Length switch
        {
            1 => CompletionResult.FromHint("all"),
            _ => CompletionResult.Empty
        };
    }
}
