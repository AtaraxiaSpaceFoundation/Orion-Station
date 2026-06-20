// SPDX-FileCopyrightText: 2026 _kote <143940725+le-kote@users.noreply.github.com>
// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Client.Lobby.UI;
using Content.Orion.Client.Lobby.UI;
using Robust.Shared.ContentPack;
using Robust.Shared.IoC;

namespace Content.Orion.Client.Entry;

public sealed class EntryPoint : GameClient
{
    public override void Init()
    {
        IoCManager.BuildGraph();
        IoCManager.InjectDependencies(this);

        HumanoidProfileEditor.CreateSpeciesWindow = context =>
        {
            var window = new SpeciesWindow(context);
            window.OpenCenteredLeft();
            return window;
        };
    }
}
