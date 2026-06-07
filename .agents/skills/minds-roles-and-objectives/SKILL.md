---
name: minds-roles-and-objectives
description: Work with minds, sessions, bodies, roles, antagonists, objectives, ownership, and transfers safely.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Minds Roles And Objectives

A session, mind, and controlled entity are related but not interchangeable.

## Ownership model

Identify whether the feature belongs to the account/session, mind, current body, role, or entity component. Store state at the layer whose lifetime matches the requirement.

## Transfers

Handle body changes, cloning, polymorph, ghosting, reconnect, possession, and administrative transfer. Do not cache the current body as permanent identity. Re-resolve the controlled entity when performing authoritative actions.

## Roles and objectives

Assign through existing role and objective systems. Prevent duplicate grants and preserve intended visibility. Hidden role/objective data must not be replicated to unrelated clients or exposed through generic UI state.

## Removal and cleanup

Define what happens on death, disconnect, role removal, objective completion, round end, and mind deletion. Remove actions, components, permissions, and subscriptions tied to the previous role or body.

## Verification

Test late join, reconnect, body transfer, cloning, ghosting, objective completion, role removal, duplicate assignment, and round cleanup.
