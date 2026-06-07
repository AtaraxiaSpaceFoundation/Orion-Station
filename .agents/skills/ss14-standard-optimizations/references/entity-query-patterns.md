<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Entity Query Patterns

## Prefer

- `EntityQueryEnumerator<...>()` for hot iteration
- querying only the components you need
- state components or marker components for fast filtering

## Rule

Do not rebuild a broad polling loop when an existing event or narrower query can already drive the behavior.
