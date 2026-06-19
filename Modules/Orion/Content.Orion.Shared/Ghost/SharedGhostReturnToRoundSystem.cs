// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using Content.Orion.Shared.CCVar;
using Robust.Shared.Configuration;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Timing;

namespace Content.Orion.Shared.Ghost;

public abstract partial class SharedGhostReturnToRoundSystem : EntitySystem
{
    [Dependency] protected IConfigurationManager Cfg = default!;
    [Dependency] protected IGameTiming GameTiming = default!;

    public override void Initialize()
    {
        base.Initialize();

        Cfg.OnValueChanged(OrionCCVars.GhostRespawnEnabled,
            ghostRespawnEnabled =>
            {
                GhostRespawnEnabled = ghostRespawnEnabled;
            },
            true);

        Cfg.OnValueChanged(OrionCCVars.GhostRespawnTime,
            ghostRespawnTime =>
            {
                GhostRespawnTime = TimeSpan.FromSeconds(Math.Max(0f, ghostRespawnTime));
            },
            true);
    }

    protected bool GhostRespawnEnabled = true;
    protected TimeSpan GhostRespawnTime = new(0, 5, 0);

    protected static string FormatTimeLeft(TimeSpan timeLeft)
    {
        var clampedTime = timeLeft > TimeSpan.Zero ? timeLeft : TimeSpan.Zero;
        var totalMinutes = (int) clampedTime.TotalMinutes;
        var seconds = clampedTime.Seconds;

        return $"{totalMinutes:00}:{seconds:00}";
    }
}
