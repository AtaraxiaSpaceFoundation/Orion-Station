<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion integration-test guidance

This project owns integrated client/server tests for Orion behavior.

Reference `Content.Benchmarks` and only Orion Client, Server, Shared, or Common projects required at compile time. Runtime-loaded modules do not grant compile-time type access.

Use `GameTest` and `SidedDependency` for real server/client environments. Assert authority first and replicated client state when part of the contract. Use simulation ticks and pair helpers instead of wall-clock sleeps.

Every test must contain observable assertions, avoid order dependencies, and clean up maps, entities, sessions, subscriptions, configuration, and global state beyond normal fixture cleanup.

Localization and culture tests must treat `en-US` as structural truth. Verify `ru-RU` message IDs, attributes, variables, selectors, relative file paths, and ordered positions, plus fallback behavior, repeated switching, saved invalid-culture fallback, and subscription cleanup.

Do not create another Orion test project, PoolManager fixture, CI step, or `module.yml` entry.

Run the exact existing project build and test commands from current workflows. If the project moves or is renamed, update the solution and dedicated CI step together.
