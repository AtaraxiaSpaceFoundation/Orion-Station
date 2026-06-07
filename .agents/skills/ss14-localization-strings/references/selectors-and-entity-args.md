<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Selectors And Entity Args

## Use FTL For Grammar

- plural forms
- selectors
- entity-aware helpers such as article and capitalization functions

## Anchor

- `Resources/Locale/en-US/wieldable/wieldable-component.ftl`

## Pairing Anchor

- `Content.Shared/Wieldable/SharedWieldableSystem.cs`

## Reminder

If the text depends on count, actor, or item entity grammar, solve it in FTL rather than branching into many hardcoded strings.
