---
name: performance
description: Review hot paths, allocations, iteration, caching, event frequency, and correctness-preserving optimization.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Performance

Do not optimize from intuition alone. Identify a measured or clearly high-frequency path.

Review:

- `Update()` frequency;
- broad entity queries;
- per-tick allocations;
- LINQ and closures;
- repeated prototype or localization lookups;
- dictionary creation;
- network payload frequency;
- unnecessary dirtying;
- repeated pathfinding or atmos work;
- client rendering allocations.

Prefer established ECS queries, cached dependencies, reused buffers, event-driven updates, and narrower dirtying. Preserve behavior, prediction, and lifecycle safety.

Add a benchmark, profiler evidence, targeted test, or at least a reasoned before/after operation count when practical.
