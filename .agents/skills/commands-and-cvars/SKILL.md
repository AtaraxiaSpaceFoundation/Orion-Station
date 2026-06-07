---
name: commands-and-cvars
description: Implement console commands and CVar-backed configuration with validation, authority, defaults, and operational safety.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Commands And Cvars

## Console commands

Keep parsing, authorization, validation, execution, and feedback distinct. Use current command interfaces and shell APIs. Validate argument count and format before mutation. Prefer calling an owning system API over modifying components directly.

Commands that affect players, round state, persistence, or server configuration require explicit permission and audit logging.

## CVars

Use the repository's typed CVar definition pattern. Choose a stable name, safe default, correct flags, and documented unit. Consider whether the value is server-only, replicated, archived, or runtime-changeable.

Subscribe to changes only when live updates are supported. Unsubscribe when the owning system shuts down. Validate bounds before applying values to timers, rates, memory sizes, or economy values.

## Compatibility

CVar names and command names are operational APIs used by configs, scripts, and administrators. Renaming requires a compatibility or migration plan.

## Verification

Test missing arguments, invalid values, unauthorized use, boundary values, config reload, runtime changes, and command help output.
