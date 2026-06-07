---
name: damage-status-and-effects
description: Implement damage, healing, status effects, alerts, movement modifiers, and timed gameplay effects consistently.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Damage Status And Effects

Use the owning damage, status, movement, and alert systems instead of mutating several components independently.

## Damage and healing

Choose typed damage specifications and existing APIs. Define armor, resistances, healing limits, dead/critical behavior, and event hooks. Avoid negative-damage shortcuts when a healing API expresses intent more clearly.

## Status effects

Define source, duration, stacking, refresh, maximum intensity, removal, and immunity. Timed effects must handle deletion, round restart, and repeated application. Use components or status APIs according to nearby patterns.

## Movement and actions

When an effect changes speed, interaction, actions, vision, or input, subscribe to the established modifier events and refresh calculated state. Do not permanently overwrite base values.

## Alerts and presentation

Keep player alerts, icons, sounds, and popups synchronized with authoritative state. Localize visible text and avoid duplicate predicted feedback.

## Common failures

- effect stacks without bound;
- timer removes a newer replacement effect;
- movement modifier is not recalculated after removal;
- client alert remains after server state ends;
- damage bypasses existing events or resistances;
- death or critical state is handled inconsistently.

## Verification

Test apply, refresh, stack, expire, remove, immunity, deletion, reconnect, death/critical state, and interactions with existing modifiers.
