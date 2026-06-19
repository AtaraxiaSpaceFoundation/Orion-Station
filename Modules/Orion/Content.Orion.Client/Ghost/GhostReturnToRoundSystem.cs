// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using Content.Client.UserInterface.Systems.Ghost.Widgets;
using Content.Orion.Shared.Ghost;
using Content.Shared.Ghost;
using Robust.Client.Player;
using Robust.Client.UserInterface;
using Robust.Shared.IoC;
using Robust.Shared.Timing;

namespace Content.Orion.Client.Ghost;

public sealed partial class GhostReturnToRoundSystem : SharedGhostReturnToRoundSystem
{
    [Dependency] private IUserInterfaceManager _userInterfaceManager = default!;
    [Dependency] private IPlayerManager _playerManager = default!;
    [Dependency] private IGameTiming _gameTiming = default!;

    private TimeSpan _lastTimeLeft = TimeSpan.Zero;

    public override void FrameUpdate(float frameTime)
    {
        base.FrameUpdate(frameTime);

        var player = _playerManager.LocalSession?.AttachedEntity;
        if (player == null)
            return;

        if (!TryComp<GhostComponent>(player, out var ghostComponent))
            return;

        var ui = _userInterfaceManager.GetActiveUIWidgetOrNull<GhostGui>();
        if (ui == null)
            return;

        var timeOffset = _gameTiming.CurTime - ghostComponent.TimeOfDeath;
        var rawTimeLeft = GhostRespawnTime - timeOffset;
        var timeLeft = rawTimeLeft > TimeSpan.Zero ? rawTimeLeft : TimeSpan.Zero;
        var canReturn = timeLeft == TimeSpan.Zero;

        var displayTime = FormatTimeLeft(timeLeft);

        var buttonStateChanged = ui.ReturnToRound.Disabled == canReturn;
        var timeChanged = FormatTimeLeft(_lastTimeLeft) != displayTime;

        if (!buttonStateChanged && !timeChanged)
            return;

        ui.ReturnToRound.Disabled = !canReturn;
        ui.ReturnToRound.Text = canReturn
            ? Loc.GetString("ghost-gui-return-to-round-ready-button")
            : Loc.GetString("ghost-gui-return-to-round-button", ("time", displayTime));

        _lastTimeLeft = timeLeft;
    }

    private static string FormatTimeLeft(TimeSpan timeLeft)
    {
        var totalMinutes = (int) timeLeft.TotalMinutes;
        var seconds = timeLeft.Seconds;

        return $"{totalMinutes:00}:{seconds:00}";
    }
}
