<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Integration test guidance

Use integration tests for behavior that crosses server, client, networking, maps, prototypes, or multiple systems.

Control time and randomness. Assert authoritative results and, when relevant, replicated client state. Keep setup minimal and clean up spawned entities or sessions. Add an integration test when a regression cannot be represented faithfully as a focused unit test.
