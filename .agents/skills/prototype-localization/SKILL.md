---
name: prototype-localization
description: Connect prototypes, entity names, descriptions, markings, datasets, and selector-driven text to FTL.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Prototype Localization

Prototype localization is part of the prototype contract, not an optional polish step.

## Workflow

1. Identify every prototype field or automatic convention that resolves through locale.
2. Find the owning module locale root.
3. Add stable FTL keys with a feature-specific prefix.
4. Provide variables required by selectors or grammar helpers.
5. Verify names, descriptions, suffixes, marking text, datasets, and UI labels separately.
6. Search for duplicate or stale keys after renames.

## Design rules

Do not derive logic from localized text. Prototype IDs remain machine identifiers and must not be shown directly to players. Keep one key per semantic meaning even when two English strings currently match.

When a prototype inherits display text, confirm that inheritance is intentional. Abstract parents should not accidentally expose placeholder text to spawnable children.

## Russian localization

Write natural Russian phrasing. Remove article wrappers such as `THE(...)`. Preserve variable meaning rather than English word order.

## Verification

Run YAML/locale validation, inspect missing-key output, and test selectors with representative entities and values.
