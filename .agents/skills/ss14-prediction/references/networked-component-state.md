<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Networked Component State

## Rules

- replicated predicted state belongs in shared components
- use `NetworkedComponent`, `AutoGenerateComponentState`, and `AutoNetworkedField` where appropriate
- dirty authoritative changes immediately

## Example Anchors

- `Content.Shared/Wieldable/Components/WieldableComponent.cs`
- `Content.Shared/Anomaly/Components/InnerBodyAnomalyComponent.cs`
