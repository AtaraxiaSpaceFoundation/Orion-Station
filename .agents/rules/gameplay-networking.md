<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Gameplay networking

The server is authoritative for protected, persistent, hidden, or competitive state.

Shared code may contain replicated components, shared events, BUI contracts, and prediction-compatible logic. Client code renders state and provides immediate feedback. Server code validates requests and owns final outcomes.

For every client-originated action:

- validate actor, target, distance, permissions, state, and rate limits on the server;
- avoid trusting client-provided entity identity, prices, quantities, access, or results;
- send the smallest required payload;
- use `NetEntity` rather than raw `EntityUid` in network payloads where the current API requires it;
- dirty replicated state after authoritative mutation;
- account for predicted execution and duplicate effects.

Do not replicate hidden information merely because the client UI would be easier to implement. Build a limited state object or explicit response instead.
