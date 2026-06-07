<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# SS14 Shared

Use this when editing `Content.Shared/`.

- Load `ss14-ecs-components`, `ss14-ecs-entities`, `ss14-ecs-systems`, `ss14-events`, `ss14-prediction`, `ss14-netcode`.
- Also load `ss14-localization-code` when the shared code emits player text or stores `LocId`.
- Shared owns replicated state, shared events, and predicted logic.
- Do not add server-only or client-only dependencies here.
