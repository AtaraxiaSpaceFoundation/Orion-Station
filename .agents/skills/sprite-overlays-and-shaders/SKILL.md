---
name: sprite-overlays-and-shaders
description: Implement sprite layers, overlays, render targets, shader prototypes, and visual cleanup without leaking gameplay authority.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Sprite Overlays And Shaders

Use visual systems for presentation derived from replicated or predicted state.

## Sprite layers

Prefer stable layer keys. Reuse layers instead of recreating them every update. Verify RSI paths, state names, visibility, color, scale, rotation, and animation behavior.

## Overlays

Define clear lifetime ownership. Add overlays only while needed and remove them on component shutdown, state exit, map transfer, or client disconnect. Avoid global overlays for entity-local effects.

## Shaders

Use shader prototypes and material parameters where current client APIs expect them. Do not update expensive parameters every frame unless the value actually changes. Treat render targets and viewport size as lifecycle-sensitive resources.

## Boundaries

Do not move gameplay checks or hidden state into client visual code. Visuals may smooth or predict presentation but must converge to authoritative state.

## Verification

Test late join, reconnect, component add/remove, entity deletion, map transfer, multiple simultaneous effects, resolution changes, and missing resource fallback.
