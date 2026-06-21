// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Content.Shared.Players.PlayTimeTracking;
using Content.Shared.Roles;
using Robust.Shared.IoC;
using Robust.Shared.Localization;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Shared.CustomGhost;

// Omitting CustomGhost prefix for convenience when prototyping
[DataDefinition]
public sealed partial class CkeyRestriction : CustomGhostRestriction
{
    [DataField(required: true)]
    public List<string> Ckey = new();

    public override bool HideOnFail => true;

    public override bool CanUse(ICommonSession player, [NotNullWhen(false)] out string? failReason)
    {
        failReason = null;
        var name = player.Name;
        if (name.StartsWith("localhost@", StringComparison.OrdinalIgnoreCase))
            name = name[10..];

        foreach (var testCkey in Ckey)
        {
            if (string.Equals(testCkey, name, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        failReason = Loc.GetString("custom-ghost-fail-exclusive-ghost");
        return false;
    }
}

[DataDefinition]
public sealed partial class PlaytimeServerRestriction : CustomGhostRestriction
{
    [DataField(required: true)]
    public float HoursPlaytime;

    private static ISharedPlaytimeManager? _playtime;

    public override bool CanUse(ICommonSession player, [NotNullWhen(false)] out string? failReason)
    {
        _playtime ??= IoCManager.Resolve<ISharedPlaytimeManager>();

        failReason = null;
        if (!_playtime.TryGetTrackerTimes(player, out var playtimes))
        {
            failReason = Loc.GetString("custom-ghost-fail-playtime-unavailable");
            return false;
        }

        double jobPlaytime = 0;
        if (playtimes.TryGetValue(PlayTimeTrackingShared.TrackerOverall, out var time))
            jobPlaytime += time.TotalHours;

        if (!(jobPlaytime < HoursPlaytime))
            return true;

        failReason = Loc.GetString("custom-ghost-fail-server-insufficient-playtime",
            ("requiredHours", MathF.Round(HoursPlaytime)),
            ("requiredMinutes", MathF.Round(HoursPlaytime % 1 * 60)),
            ("playtimeHours", Math.Round(jobPlaytime)),
            ("playtimeMinutes", Math.Round(jobPlaytime % 1 * 60))
        );

        return false;

    }
}

[DataDefinition]
public sealed partial class PlaytimeJobRestriction : CustomGhostRestriction
{
    [DataField(required: true)]
    public ProtoId<JobPrototype> Job = string.Empty;

    [DataField(required: true)]
    public float HoursPlaytime;

    private static ISharedPlaytimeManager? _playtime;
    private static IPrototypeManager? _proto;

    public override bool CanUse(ICommonSession player, [NotNullWhen(false)] out string? failReason)
    {
        _playtime ??= IoCManager.Resolve<ISharedPlaytimeManager>();
        _proto ??= IoCManager.Resolve<IPrototypeManager>();

        failReason = null;
        if (!_playtime.TryGetTrackerTimes(player, out var playtimes))
        {
            failReason = Loc.GetString("custom-ghost-fail-playtime-unavailable");
            return false;
        }

        double jobPlaytime = 0;
        if (!_proto.TryIndex(Job, out var jobProto))
        {
            failReason = Loc.GetString("custom-ghost-fail-invalid-job");
            return false;
        }

        if (playtimes.TryGetValue(jobProto.PlayTimeTracker, out var time))
            jobPlaytime += time.TotalHours;

        if (!(jobPlaytime < HoursPlaytime))
            return true;

        failReason = Loc.GetString("custom-ghost-fail-job-insufficient-playtime",
            ("job", Loc.GetString(jobProto.Name)),
            ("requiredHours", MathF.Round(HoursPlaytime)),
            ("requiredMinutes", MathF.Round(HoursPlaytime % 1 * 60)),
            ("playtimeHours", Math.Round(jobPlaytime)),
            ("playtimeMinutes", Math.Round(jobPlaytime % 1 * 60))
        );

        return false;

    }
}

[DataDefinition]
public sealed partial class PlaytimeDepartmentRestriction : CustomGhostRestriction
{
    [DataField(required: true)]
    public ProtoId<DepartmentPrototype> Department = string.Empty;

    [DataField(required: true)]
    public float HoursPlaytime;

    private static ISharedPlaytimeManager? _playtime;
    private static IPrototypeManager? _proto;

    public override bool CanUse(ICommonSession player, [NotNullWhen(false)] out string? failReason)
    {
        _playtime ??= IoCManager.Resolve<ISharedPlaytimeManager>();
        _proto ??= IoCManager.Resolve<IPrototypeManager>();

        failReason = null;
        if (!_playtime.TryGetTrackerTimes(player, out var playtimes))
        {
            failReason = Loc.GetString("custom-ghost-fail-playtime-unavailable");
            return false;
        }

        double departmentPlaytime = 0;
        if (!_proto.TryIndex(Department, out var departmentProto))
        {
            failReason = Loc.GetString("custom-ghost-fail-invalid-department");
            return false;
        }

        var departmentJobs = departmentProto.Roles;
        foreach (var job in departmentJobs)
        {
            if (!_proto.TryIndex(job, out var jobProto))
            {
                failReason = Loc.GetString("custom-ghost-fail-invalid-job");
                return false;
            }

            if (playtimes.TryGetValue(jobProto.PlayTimeTracker, out var time))
                departmentPlaytime += time.TotalHours;
        }

        if (!(departmentPlaytime < HoursPlaytime))
            return true;

        failReason = Loc.GetString("custom-ghost-fail-department-insufficient-playtime",
            ("department", Loc.GetString(departmentProto.Name)),
            ("requiredHours", MathF.Round(HoursPlaytime)),
            ("requiredMinutes", MathF.Round(HoursPlaytime % 1 * 60)),
            ("playtimeHours", Math.Round(departmentPlaytime)),
            ("playtimeMinutes", Math.Round(departmentPlaytime % 1 * 60))
        );

        return false;
    }
}
