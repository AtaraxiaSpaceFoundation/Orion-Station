---
name: admin-and-permissions
description: Implement privileged commands, admin UI, role checks, audit logs, and safe operational controls.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Admin And Permissions

Privileged tooling must fail closed and remain server-authoritative.

## Workflow

1. Identify the exact permission or admin flag required.
2. Enforce it on the server at every entry point.
3. Separate read-only diagnostics from mutating operations.
4. Validate targets and current round/session state.
5. Record meaningful audit context for destructive or player-affecting actions.
6. Return localized, user-safe feedback without exposing secrets.

## Boundaries

Client visibility of a button is not authorization. Console command registration, EUI/BUI messages, verbs, and network events must each reach a server-side permission check.

Do not broaden permissions because a nearby command uses a more general flag. Avoid commands that bypass normal system APIs and leave state inconsistent.

## Auditability

Log actor, operation, target, important parameters, and outcome for high-impact changes. Do not log credentials, tokens, private chat content, or unnecessary personal data.

## Verification

Test authorized and unauthorized users, missing sessions, disconnected targets, repeated execution, malformed arguments, and runtime permission changes.
