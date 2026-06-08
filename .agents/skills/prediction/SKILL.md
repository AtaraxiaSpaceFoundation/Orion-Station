---
name: prediction
description: Keep responsive shared interactions deterministic and free of duplicated side effects while server authority wins.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Prediction

Prediction provides immediate local feedback while the server remains authoritative.

Predict only short player-driven interactions with locally known inputs and correctable results. Keep hidden information, economy outcomes, permissions, access checks, and persistence authoritative.

Shared predicted code may execute more than once. Separate deterministic state changes from one-shot effects and avoid non-deterministic randomness in predicted paths.

Predicted player feedback requires English localization and an ordered Russian counterpart. Resolving the same key twice is acceptable. Showing the feedback twice is not.

Verify repository ownership and edit-marker requirements before changing inherited files.

Run the Debug build, root integration tests, and every affected module integration project. Test latency, rejection, repeated input, observers, deletion, and reconciliation.
