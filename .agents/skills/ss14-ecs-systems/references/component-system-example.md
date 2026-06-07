<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Component And System Example

## Example Anchor

Use the wieldable feature as a reference pair:

- Component: `Content.Shared/Wieldable/Components/WieldableComponent.cs`
- Shared system: `Content.Shared/Wieldable/SharedWieldableSystem.cs`
- Server system: `Content.Server/Wieldable/WieldableSystem.cs`
- Client system: `Content.Client/Wieldable/WieldableSystem.cs`
- Locale: `Resources/Locale/en-US/wieldable/wieldable-component.ftl`

## Why It Is Useful

- The component stays data-focused.
- The shared system owns interaction and prediction-aware behavior.
- Client and server specialize only where needed.
- The feature has direct FTL pairing for player-facing feedback.
