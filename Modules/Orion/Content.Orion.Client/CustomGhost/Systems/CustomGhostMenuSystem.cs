// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Orion.Client.CustomGhost.UI;
using Robust.Client.UserInterface;
using Robust.Shared.Console;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Orion.Client.CustomGhost.Systems;

public sealed partial class CustomGhostMenuSystem : EntitySystem
{
    [Dependency] private IConsoleHost _consoleHost = default!;
    [Dependency] private IUserInterfaceManager _uiManager = default!;

    private CustomGhostsWindow? _customGhostWindow;

    public override void Initialize()
    {
        base.Initialize();

        _consoleHost.RegisterCommand(
            "customghosts",
            Loc.GetString("custom-ghosts-command-open-desc"),
            Loc.GetString("custom-ghosts-command-open-help"),
            OpenCommand);
    }

    public override void Shutdown()
    {
        _consoleHost.UnregisterCommand("customghosts");
        _customGhostWindow?.Close();
        _customGhostWindow = null;

        base.Shutdown();
    }

    private void OpenCommand(IConsoleShell shell, string argStr, string[] args)
    {
        if (_customGhostWindow is { Disposed: false })
        {
            _customGhostWindow.Close();
            return;
        }

        _customGhostWindow = _uiManager.CreateWindow<CustomGhostsWindow>();
        _customGhostWindow.OnClose += () => _customGhostWindow = null;
        _customGhostWindow.OpenCenteredLeft();
    }
}
