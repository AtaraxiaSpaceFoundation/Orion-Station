<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Predicted Feedback

## Prefer

- `PopupPredicted`
- `PopupClient`
- `PlayPredicted`
- predicted spawn and delete helpers

## Example Anchor

- `Content.Shared/Wieldable/SharedWieldableSystem.cs`

## Rule

If the local player should feel the action immediately, do not leave the feedback path server-only by accident.
