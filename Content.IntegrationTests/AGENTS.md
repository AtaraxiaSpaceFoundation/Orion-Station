<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Content.IntegrationTests Agent Notes

Read [../AGENTS.md](../AGENTS.md) first.

For `Content.IntegrationTests` work always load:

- `ss14-tests-authoring`
- `ss14-naming-conventions`
- `ss14-upstream-maintenance`

Use integration tests when the behavior depends on runtime orchestration, entity lifecycle, prediction, or server-client interaction. Keep them focused and avoid escalating from a simpler content test without need.
