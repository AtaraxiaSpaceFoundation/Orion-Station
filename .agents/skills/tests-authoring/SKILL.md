---
name: tests-authoring
description: Design deterministic focused and integration tests that reproduce real regressions and exercise authoritative behavior.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Tests Authoring

A useful test proves an observable contract and fails for the original bug.

## Focused tests

Use focused tests for pure validation, serialization, prototype assumptions, system APIs, and deterministic state transitions. Keep setup small and assert the behavior that callers depend on.

## Integration tests

Use integration tests when networking, server/client state, maps, timers, multiple systems, or lifecycle integration matters. Control simulation time and randomness. Inspect both authoritative and replicated state when testing desyncs.

## Structure

Name tests by behavior. Arrange only required entities and prototypes. Act through the public API or real event path. Assert success and important rejection branches. Clean up sessions, maps, and spawned entities.

## Regression quality

Avoid tests that duplicate the implementation line by line. Prefer boundary values, repeated invocation, deletion, cancellation, and malformed input where those caused the bug.

## Reliability

Do not use wall-clock sleeps. Avoid dependence on test order, global state, live network services, or machine locale.

## Verification

Run the narrow test during iteration, then the owning test project. Report filters and arguments exactly.
