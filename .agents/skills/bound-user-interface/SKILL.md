---
name: bound-user-interface
description: Implement BUI keys, shared messages and state, server handlers, client windows, validation, and state updates.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Bound User Interface

A BUI crosses Shared, Server, Client, and often Resources.

## Shared

Define the UI key, serializable messages, and minimal state. Messages express intent, not trusted results.

## Server

Register handlers, validate the actor and target, derive authoritative values, mutate state, and send updated UI state. Re-check access, distance, ownership, and current values.

## Client

Create the BUI class and window, bind state to controls, and send intent messages. Client checks are for UX only.

## Common failures

- trusting a displayed price or selected account;
- sending complete server components as UI state;
- forgetting to refresh state after mutation;
- duplicate subscriptions after reopening;
- stale UI after deletion or range loss;
- hardcoded labels.

Test open, update, rejected message, close, deletion, and multiple viewers.
