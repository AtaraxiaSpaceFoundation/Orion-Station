<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Common performance pitfalls

- global scans in frequent events;
- allocating temporary collections every tick;
- repeated `TryIndex` for stable prototypes;
- dirtying unchanged state;
- sending large BUI state on every minor input;
- pathfinding requests without cooldown or cancellation;
- per-tile atmos work added outside existing batching;
- client overlays recreated instead of updated;
- logging inside hot loops.

A faster implementation that changes authoritative or predicted behavior is a regression, not an optimization.
