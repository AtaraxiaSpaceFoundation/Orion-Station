---
name: entity-lifecycle-and-spawning
description: Spawn, initialize, replace, transfer, and delete entities without breaking lifecycle, transforms, containers, or ownership.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Entity Lifecycle And Spawning

Entity creation and removal affect components, maps, containers, networking, and cleanup.

## Spawning

Choose the spawn API from placement semantics: attached to an entity, placed on a grid, positioned in map coordinates, or created for a container/system owner. Prefer prototype-driven initialization and existing system helpers.

Do not spawn from the client for authoritative gameplay. Do not manually reconstruct a prototype's component set in code unless runtime composition is the intended design.

## Initialization

Account for component initialization, startup, map initialization, and system subscriptions. Values needed before map initialization must be available at the correct earlier lifecycle point. Avoid assuming optional components exist merely because common prototypes include them.

## Replacement and transfer

When replacing an entity, define which state transfers: position, rotation, ownership, mind, hands, containers, damage, name, access, UI sessions, and references held by other systems. Use established replacement or polymorph APIs when they preserve required invariants.

## Deletion

Use deferred deletion when dispatch, iteration, or physics callbacks make immediate removal unsafe. Remove timers, overlays, child entities, actions, UI sessions, and cached references. Handle repeated deletion requests idempotently.

## Common failures

- spawn occurs in the wrong coordinate space;
- map initialization is skipped or repeated;
- contained entity is deleted without container cleanup;
- replacement loses mind or inventory unexpectedly;
- event handler keeps using an entity after deletion;
- client and server spawn duplicate presentation entities.

## Verification

Test map and grid placement, containers, late initialization, replacement, deletion during events, map transfer, reconnect, and cleanup of dependent state.
