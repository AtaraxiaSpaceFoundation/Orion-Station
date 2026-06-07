<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

---
name: ss14-client-server-shared
description: Learn or reason about the SS14 split between `Content.Client`, `Content.Shared`, and `Content.Server`. Use when deciding where code belongs, explaining prediction, understanding client/server trust boundaries, or mapping a feature across replication and networking.
---

# SS14 Client Server Shared

Use this skill for assembly ownership, prediction mental models, and networking basics.

## Workflow

1. Open `references/client-server-primer.md`.
2. Open `references/shared-and-prediction.md`.
3. Open `references/networking-and-dirty.md` when a change involves replicated state or network events.
4. Keep server authority and client responsiveness separate in your reasoning.

## Reference Map

- `references/client-server-primer.md`
- `references/shared-and-prediction.md`
- `references/networking-and-dirty.md`
- `../ss14-netcode/references/network-event-patterns.md`
- `../ss14-prediction/references/networked-component-state.md`
