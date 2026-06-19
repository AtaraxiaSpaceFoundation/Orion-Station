// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using Content.Client.UserInterface.Systems.Ghost.Widgets;
using Content.Orion.Shared.Ghost;
using Content.Shared.Ghost;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.IoC;
using Robust.Shared.Timing;

namespace Content.Orion.Client.Ghost;

public sealed partial class GhostReturnToRoundSystem : SharedGhostReturnToRoundSystem
{
    [Dependency] private IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private IPlayerManager _playerManager = default!;
    [Dependency] private IGameTiming _gameTiming = default!;

    private TimeSpan _lastTimeLeft = TimeSpan.Zero;
    private GhostGui? _trackedGui;
    private Button? _returnToRoundButton;

    public override void FrameUpdate(float frameTime)
    {
        base.FrameUpdate(frameTime);

        if (!GhostRespawnEnabled)
        {
            RemoveReturnToRoundButton();
            return;
        }

        var player = _playerManager.LocalSession?.AttachedEntity;
        if (player == null || !TryComp<GhostComponent>(player, out var ghostComponent))
        {
            RemoveReturnToRoundButton();
            return;
        }

        var ui = _userInterfaceManager.GetActiveUIWidgetOrNull<GhostGui>();
        if (ui == null)
        {
            RemoveReturnToRoundButton();
            return;
        }

        if (!EnsureReturnToRoundButton(ui) || _returnToRoundButton == null)
            return;

        var button = _returnToRoundButton;
        var timeOffset = _gameTiming.RealTime - ghostComponent.TimeOfDeath;
        var rawTimeLeft = GhostRespawnTime - timeOffset;
        var timeLeft = rawTimeLeft > TimeSpan.Zero ? rawTimeLeft : TimeSpan.Zero;
        var canReturn = timeLeft == TimeSpan.Zero;

        var displayTime = FormatTimeLeft(timeLeft);

        var buttonStateChanged = button.Disabled == canReturn;
        var timeChanged = FormatTimeLeft(_lastTimeLeft) != displayTime;

        if (!buttonStateChanged && !timeChanged)
            return;

        button.Disabled = !canReturn;
        button.Text = canReturn
            ? Loc.GetString("ghost-gui-return-to-round-ready-button")
            : Loc.GetString("ghost-gui-return-to-round-button", ("time", displayTime));

        _lastTimeLeft = timeLeft;
    }

    public override void Shutdown()
    {
        base.Shutdown();

        RemoveReturnToRoundButton();
    }

    private bool EnsureReturnToRoundButton(GhostGui ui)
    {
        if (_trackedGui == ui && _returnToRoundButton != null)
            return true;

        RemoveReturnToRoundButton();

        _trackedGui = ui;
        _returnToRoundButton = new Button
        {
            Disabled = true,
            Text = Loc.GetString("ghost-gui-return-to-round-ready-button"),
            ToolTip = Loc.GetString("ghost-gui-return-to-round-button-tooltip"),
        };
        _returnToRoundButton.OnPressed += OnReturnToRoundPressed;
        ui.AddGhostButton(_returnToRoundButton);

        return true;
    }

    private void RemoveReturnToRoundButton()
    {
        if (_returnToRoundButton == null)
        {
            _trackedGui = null;
            return;
        }

        _returnToRoundButton.OnPressed -= OnReturnToRoundPressed;
        _returnToRoundButton.Parent?.RemoveChild(_returnToRoundButton);
        _returnToRoundButton = null;
        _trackedGui = null;
    }

    private void OnReturnToRoundPressed(BaseButton.ButtonEventArgs args)
    {
        RaiseNetworkEvent(new GhostReturnToRoundRequest());
    }

}
