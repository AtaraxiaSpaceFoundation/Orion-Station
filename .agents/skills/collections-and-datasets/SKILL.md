---
name: collections-and-datasets
description: Maintain weighted collections, datasets, groups, selectors, and random content tables.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Collections And Datasets

Collections are data-driven selection APIs. Preserve their statistical and compatibility behavior.

## Workflow

1. Find the prototype type and every consumer.
2. Determine whether entries are weighted, unique, ordered, nested, or filtered.
3. Verify all referenced prototype IDs or locale keys.
4. Keep empty and single-entry behavior defined.
5. Review duplicate entries and unintended weight changes.
6. Add deterministic tests where selection logic is custom.

## Randomness

Use the owning server or shared random service according to authority and prediction requirements. Do not introduce client-selected outcomes for protected gameplay. Predicted random choices require a deterministic source or authoritative correction.

## Localization

Datasets containing player-visible strings should resolve through FTL. Do not embed raw display text in code when a dataset prototype is appropriate.

## Verification

Check normalization assumptions, fallback behavior, nested collection recursion, invalid references, and distribution-sensitive balance changes.
