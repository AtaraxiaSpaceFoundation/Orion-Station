---
name: ecs-components
description: Design SS14 components as serialized state in the correct assembly.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Ecs Components

Components are data attached to entities. Keep behavior in systems.

## Workflow

1. Find a nearby component owned by the same subsystem.
2. Choose the assembly from who must read the state.
3. Add `[RegisterComponent]` and the repository's normal serialization attributes.
4. Use `[DataField]` for prototype-backed configuration.
5. Use typed IDs such as `ProtoId<T>` or `EntProtoId` where supported.
6. Add network generation attributes only for fields the client needs.
7. Define lifecycle defaults that remain valid before prototype data is applied.
8. Add tests for important validation or serialization behavior.

## Networking

A networked component belongs in Shared. Do not replicate hidden server state. After authoritative mutation, call the appropriate dirtying API from the system that owns the change.

## Avoid

- service lookup or game logic inside components;
- raw prototype strings when typed IDs exist;
- mutable collections without a clear serialization and dirtying strategy;
- client-only types in Shared;
- using `EntityUid.Invalid` instead of nullable references when the surrounding API uses nullability.
