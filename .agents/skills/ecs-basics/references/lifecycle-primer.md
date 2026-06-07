<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Lifecycle primer

Common lifecycle moments include component initialization, startup, map initialization, shutdown, removal, entity deletion, parent changes, anchoring, container insertion, and map transfer.

Choose the earliest point where required dependencies are valid and the latest point where cleanup is still safe. Do not assume a component exists merely because a prototype normally includes it. Handle deletion and removal without leaving timers, overlays, UI sessions, or cached entity references alive.
