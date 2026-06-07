---
name: networking
description: Implement network events, networked components, state generation, conversion, and dirtying.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Networking

## Network events

Define payloads in Shared and mark them with the serialization attributes used by current code. Use `NetEntity` for entity references when required. Raise and subscribe through the established network-event APIs.

Validate every received message on the server. Keep payloads minimal and version-tolerant where practical.

## Networked components

Use `[NetworkedComponent]` and the current generated component-state pattern only when the client needs replicated state. Mark only required fields. Do not replicate secrets or large mutable objects by default.

After an authoritative mutation, call `Dirty(...)` or `DirtyField(...)` according to the generated pattern. Dirtying the wrong component or forgetting dirtying produces stale client state.

## Reconciliation

Predicted code may apply a temporary local result. The authoritative state must converge without duplicate effects. Test delayed, rejected, and repeated actions.
