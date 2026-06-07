---
name: localization
description: Write and validate FTL, locale-backed prototype text, variables, and localized code.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Localization

All player-visible text must be localized.

## FTL

Use a feature-scoped key namespace and natural wording. Keep variables explicit and named consistently with code. Reuse a key only when the meaning is genuinely shared.

## Code

Use localization APIs and `LocId` according to nearby patterns. Do not compare localized output in logic. Do not show raw enum values, prototype IDs, component names, or internal errors to players.

## Prototypes

Add names, descriptions, marking names, datasets, and other locale-backed fields in the owning resource root.

## Russian

Write natural Russian. Remove article wrappers such as `THE(...)`; do not preserve English grammar markers in Russian strings.

Verify missing keys, wrong variable names, duplicate keys, and module locale loading.
