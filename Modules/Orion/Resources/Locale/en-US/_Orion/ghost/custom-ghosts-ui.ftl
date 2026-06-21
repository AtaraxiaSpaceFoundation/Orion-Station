# SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
# SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
#
# SPDX-License-Identifier: AGPL-3.0-or-later

custom-ghost-fail-exclusive-ghost = This ghost is ckey-locked.
custom-ghost-fail-playtime-unavailable = Failed to get playtimes. Ask an admin for help if this error persists.
custom-ghost-fail-invalid-job = This ghost has an invalid job requirement.
custom-ghost-fail-invalid-department = This ghost has an invalid department requirement.
custom-ghost-fail-server-insufficient-playtime = Play on the server for {$requiredHours} {$requiredHours ->
  *[one] hour
  [other] hours
}. { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }

custom-ghost-fail-job-insufficient-playtime = Play as a {$job} {$requiredHours} {$requiredHours ->
  *[one] hour
  [other] hours
}. { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }

custom-ghost-fail-department-insufficient-playtime = Play as a member of {$department} for {$requiredHours} {$requiredHours ->
  *[one] hour
  [other] hours
}. { -playtime(pH: $playtimeHours, pM: $playtimeMinutes) }

custom-ghosts-window-title = Custom ghost menu
custom-ghosts-window-show-all-checkbox = Show all
custom-ghosts-window-show-all-checkbox-tooltip = Shows ghosts that are not unlocked yet. Hover over locked entry to see its unlock requirements.
custom-ghost-window-tooltip-to-unlock = To unlock this ghost you need:

-playtime = Your current playtime is {$pH} {$pH ->
  *[one] hour
  [other] hours
} {$pM} {$pM ->
  *[one] minute
  [other] minutes
}
