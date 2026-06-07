---
name: ecs-events
description: Choose and implement local, directed, broadcast, cancellable, and network events.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Ecs Events

Use events for established ECS flows or genuine decoupling.

## Event choices

- Directed local events target one entity and its components.
- Broadcast events notify independent global listeners.
- Ref events allow listeners to mutate a result or cancellation flag.
- Handled/cancelled events need explicit ownership of who may stop the flow.
- Network events cross the trust boundary and require validation.

Prefer a direct call to the owning system when only one known owner performs the operation. Do not invent events simply to hide a dependency.

## Rules

Define event data in the lowest assembly required by publishers and subscribers. Keep network payloads serializable and small. Do not put raw server-only entities into client payloads.

Document whether an event is notification-only, cancellable, or expected to be raised before/after mutation. Tests should cover cancellation and ordering when they affect gameplay.
