---
name: construction-and-machines
description: Implement construction graphs, machine lifecycle, power, parts, upgrades, examination, and machine UI.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Construction And Machines

Machines often combine prototypes, construction, anchoring, power, components, server simulation, UI, and upgrade parts.

## Construction

Find the current graph and node conventions. Keep steps data-driven. Validate tools, materials, entity replacement, deconstruction, and state transfer. Avoid source-fork graph IDs that do not exist in the destination.

## Machine lifecycle

Define behavior for map initialization, anchoring, unanchoring, power changes, damage, breakage, deconstruction, and deletion. Systems should subscribe to lifecycle events instead of polling every machine each tick.

## Parts and upgrades

Use existing part-rating or machine-board APIs. Recalculate derived values from installed parts at the established event point. Keep prototype defaults valid when no upgraded parts are present. Do not cache a derived value without invalidation.

## UI and authority

The server owns machine state, inventory, costs, output, and access. BUI messages express intent and are revalidated. UI state should expose only values the user needs.

## Common failures

- upgraded value never recalculated after part change;
- construction replacement loses ownership or contents;
- machine runs while unpowered or unanchored;
- server trusts client-selected output or price;
- map-spawned machines skip setup;
- examine text disagrees with actual state.

## Verification

Test construction, deconstruction, map spawn, power loss, anchoring, part replacement, damage, UI concurrency, and YAML references.
