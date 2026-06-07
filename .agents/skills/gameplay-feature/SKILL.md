---
name: gameplay-feature
description: Plan and implement a complete SS14 feature across ownership, ECS, networking, resources, UI, and tests.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Gameplay Feature

Use this as the routing skill for multi-layer work.

## Plan

1. Identify owner and module.
2. Map components, systems, events, prototypes, locale, assets, UI, and tests.
3. Define trust and prediction boundaries.
4. Reuse nearby APIs.
5. Implement in small coherent layers.
6. Verify references and behavior.

## Review

- Is state in the correct component and assembly?
- Is server authority preserved?
- Are network messages minimal and validated?
- Is predicted feedback de-duplicated?
- Are prototypes and FTL complete?
- Are resources in the owning root?
- Is UI state limited and refreshed?
- Are tests appropriate to the risk?
- Did the change touch inherited code unnecessarily?

For a port, also load the porting skill and its manifest reference.
