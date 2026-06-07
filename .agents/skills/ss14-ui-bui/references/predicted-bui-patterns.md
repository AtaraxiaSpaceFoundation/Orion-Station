<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Predicted BUI Patterns

## Prefer

- reading already-networked component state when the client already has it
- `SendPredictedMessage` for predicted user actions
- updating client visuals from replicated state after it lands

## Example Anchor

- `Content.Client/Atmos/UI/GasCanisterBoundUserInterface.cs`

## Do Not

- invent duplicate BUI state without a reason
- mix server-only effects into predicted shared paths
