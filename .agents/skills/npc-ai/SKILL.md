---
name: npc-ai
description: Modify NPC behavior, HTN tasks, navigation, steering, prototypes, and AI debugging.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Npc Ai

Trace the current AI stack before adding a new behavior.

Identify:

- the owning server system;
- HTN operator, task, or utility selection;
- target acquisition;
- pathfinding and steering;
- cooldown and cancellation;
- prototype configuration;
- debug tools and tests.

Avoid expensive global searches in repeated updates. Keep behavior deterministic where tests require it. Cancel or invalidate navigation when entities, maps, targets, or goals change.

Update prototypes and integration coverage for observable behavior. Test no-target, unreachable-target, deletion, map transfer, and repeated planning.
