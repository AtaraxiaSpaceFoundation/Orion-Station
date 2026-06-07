---
name: pvs
description: Work with visibility, network interest, PVS filters, and PVS-sensitive entity behavior.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Pvs

PVS determines which entities and events a client receives. It is a bandwidth and visibility mechanism, not authorization.

## Workflow

1. Identify why the client needs the entity or event.
2. Prefer normal world visibility and existing filters.
3. Use a limited explicit message or UI state when the client needs remote data.
4. Add PVS overrides only when the entity itself must remain network-visible.
5. Remove overrides and session-specific visibility when the need ends.

## Review

Consider parent/child transforms, containers, grids, maps, ghosts, observers, sessions, and map transfer. World audio and effects normally use PVS-scoped recipients. Global delivery needs an explicit gameplay reason and recipient filter.

Avoid broad or permanent overrides that retain large entity sets. Do not expose hidden server information merely because an entity is forced into PVS.

## Verification

Test entering and leaving range, reconnecting, observing, container changes, parent changes, map transfer, deletion, and UI closure. Confirm state does not leak to unrelated sessions.
