<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# FTL Naming And Layout

## Rules

- Use specific feature-scoped `kebab-case` keys.
- Keep related keys in the same feature file when the repo already groups them that way.
- Start with `Resources/Locale/en-US/`.

## Good Anchor

- `Resources/Locale/en-US/wieldable/wieldable-component.ftl`

## Common Mistakes

- one giant generic localization file for unrelated features
- vague keys like `item-name` or `button-text`
- putting a new key in an unrelated locale folder because it happened to exist first
