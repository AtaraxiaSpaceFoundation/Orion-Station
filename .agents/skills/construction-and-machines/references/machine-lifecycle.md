<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Machine lifecycle

Review:

`prototype/map spawn -> initialization -> anchoring -> power -> user interaction -> part change -> damage/breakage -> deconstruction -> deletion`

At every transition, identify which derived values and UI states need recalculation. Prefer event-driven invalidation over repeated polling.
