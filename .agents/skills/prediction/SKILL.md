---
name: prediction
description: Design responsive shared interactions that reconcile with server authority without duplicated side effects.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Prediction

Prediction gives the local player immediate feedback while the server remains authoritative.

## Suitable work

Predict short, player-driven interactions whose inputs are locally known and whose result can be corrected. Keep hidden information, economy results, access decisions, and persistence authoritative.

## Repeat execution

Shared predicted code can execute more than once. Separate deterministic state changes from one-shot side effects. Use the current repository pattern for first-time prediction when a sound, popup, animation, or spawn must not duplicate.

Avoid non-deterministic randomness in predicted paths. Do not mutate server-only state from shared prediction code.

## Reconciliation

The server must run the same validation or reject the request. Client state should converge after authoritative updates. A predicted popup or sound should not replay when confirmation arrives.

## Testing

Test with latency, rejection, repeated input, and interactions performed by observers or non-owning clients when relevant.
