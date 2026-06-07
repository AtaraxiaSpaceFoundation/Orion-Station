<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Localization Examples

## Straightforward Gameplay Strings

- `Resources/Locale/en-US/wieldable/wieldable-component.ftl`
- `Content.Shared/Wieldable/SharedWieldableSystem.cs`

Use these as anchors for:

- `Loc.GetString(...)`
- argument substitution like `("item", uid)`
- plural or selector handling in FTL

## Reminders

- Keep the FTL key semantic rather than sentence-like.
- Put related keys in the same feature folder when possible.
