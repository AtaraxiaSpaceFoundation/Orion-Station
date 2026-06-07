<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# XAML structure

Keep named controls limited to elements code-behind actually accesses. Prefer containers and existing styles over manual pixel positioning.

Code-behind should expose methods such as `UpdateState`, translate user actions into events, and avoid owning authoritative state.

When a window is recreated or rebound, prevent duplicate event subscriptions. Verify XAML resource URIs and the owning client project's XamlIL setup.
