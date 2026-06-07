---
name: access-and-identity
description: Implement access checks, identity sources, ID cards, credentials, ownership, and authorization without trusting presentation state.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Access And Identity

Access is an authoritative gameplay decision. Determine which identity source the subsystem actually uses before adding another check.

## Identity sources

Possible sources include the current body, mind, session, ID card, PDA, inventory item, account, role, faction, job, or explicit ownership relation. These are not interchangeable. Choose the source whose lifetime and semantics match the operation.

## Validation flow

1. Resolve the acting session, mind, and controlled entity as required.
2. Resolve current credentials from authoritative state.
3. Validate target, range, ownership, access groups, and subsystem-specific restrictions.
4. Revalidate at execution time after a DoAfter, UI delay, or network round trip.
5. Return a localized reason without exposing hidden permissions.

Client UI may display likely access, but the server decides. Do not trust a client-supplied ID entity, department, role, account owner, or access list.

## Changes and invalidation

Handle card insertion/removal, equipment changes, body transfer, role changes, account reassignment, disconnect, deletion, and permission updates. Avoid caching access results longer than the underlying identity state remains valid.

## Common failures

- checking the body when the permission belongs to the mind or account;
- trusting access displayed by the client;
- stale credential cache after card removal;
- bypass through a second entry point;
- leaking hidden role or access information in error text;
- using job title as authorization when explicit access exists.

## Verification

Test authorized and unauthorized actors, missing credentials, stale UI, body transfer, card replacement, disconnect, target deletion, and repeated requests.
