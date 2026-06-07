<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Visual lifecycle

Trace visual state through:

`component start -> appearance/state update -> client visual application -> repeated update -> component removal -> entity deletion`

For overlays and child visuals, add map transfer and client reconnect. Every allocated client visual object needs a clear owner and cleanup point.

Common leaks include duplicate event subscriptions, overlays added more than once, child entities never deleted, and shader instances retained after the feature closes.
