---
name: naming-conventions
description: Name C# symbols, events, prototypes, FTL keys, resources, and migrations consistently and compatibly.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Naming Conventions

Names should communicate ownership, timing, and intent.

## C#

Use repository-standard casing and suffixes. Components and systems should be easy to find by type search. Events should state whether they are attempts, notifications, pre-change checks, or completed changes. Avoid abbreviations that are not already project vocabulary.

## Prototypes and resources

Prototype IDs are stable identifiers, not display names. Use consistent feature prefixes when collision is possible. Keep resource directory, RSI state, audio collection, and map names aligned with nearby content.

## Localization

FTL keys use lowercase kebab-case and a stable feature prefix. Variable names should reflect meaning rather than UI position.

## Compatibility

Before renaming a serialized field, prototype ID, map entity, database field, CVar, or network message, search save data, maps, migrations, config files, and downstream references. Provide a migration or compatibility alias when required.
