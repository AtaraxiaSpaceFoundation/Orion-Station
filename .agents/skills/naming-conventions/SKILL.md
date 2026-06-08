---
name: naming-conventions
description: Name C# symbols, events, prototypes, FTL keys, resources, and compatibility surfaces consistently.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Naming Conventions

Names must communicate ownership, timing, and intent.

## C#

Use repository-standard casing and suffixes. Components and systems should be discoverable by type search. Events should state whether they are attempts, pre-change checks, notifications, or completed changes.

Do not introduce an abbreviation that is not established project vocabulary.

## Prototypes and resources

Prototype IDs are stable machine identifiers, not display names. Use a feature prefix where collisions are possible. Keep directory names, RSI states, audio collections, map names, and nearby prototypes aligned.

## Localization

FTL keys use lowercase kebab-case and a stable owner or feature prefix. Variable names describe meaning, not UI position.

Use the same keys and variables in `en-US` and `ru-RU`. A module key prefix must make ownership clear. File placement does not protect duplicate keys.

## Compatibility

Before renaming a serialized field, prototype ID, map entity, database field, CVar, locale key, or network message, search code, prototypes, maps, migrations, config, both cultures, and downstream references. Provide a migration or compatibility alias where required.

## Verification commands

```powershell
git grep -n "OLD_NAME" -- .
git grep -n "NEW_NAME" -- .
dotnet build --configuration Debug --no-restore /m
git diff --check
```

For prototype or locale renames also run the Release build and YAML linter from `yaml-and-schema`.
