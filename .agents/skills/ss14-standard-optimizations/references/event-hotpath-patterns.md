<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Event Hot Path Patterns

## Prefer

- thin handlers
- early returns
- existing refresh events over new polling loops
- avoiding allocations in high-frequency paths

## Example Anchor

- `Content.Shared/Wieldable/SharedWieldableSystem.cs`
