---
name: localization
description: Maintain English-source localization with ordered Russian parity, existing owner paths, correct variables, selectors, and runtime behavior.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Localization

Localization is part of feature implementation.

## Mandatory discovery

Before editing:

1. enumerate the relevant `en-US` and `ru-RU` files
2. search exact, old, alternative, and proposed keys
3. identify the existing owner file for the feature
4. inspect its mirrored counterpart
5. inspect code, XAML, prototypes, and maps using the keys

Never invent a path from a repository or module name. Preserve exact underscores, casing, grouping, and relative paths.

## English is the source of truth

English defines key identity, attributes, variables, selectors, file placement, section grouping, and message order.

When English changes:

- add the Russian counterpart at the same relative position
- remove the Russian counterpart when English removes it
- rename the Russian key when English renames it
- move or reorder Russian messages when English moves or reorders them
- mirror changed attributes, variables, and selectors exactly
- mirror English file moves or renames

Do not append new Russian keys to the end unless the corresponding English key is at the end.

A Russian wording-only correction may leave English unchanged when the structural contract is identical.

A Russian-only key is stale unless a verified framework requirement or documented convention proves otherwise.

## Translation requirements

Write natural Russian. Do not copy English as a placeholder. Do not use `THE(...)` or equivalent English grammar markers.

Preserve placeholders, markup, line breaks, and control hints required by code.

## Renames and deletions

English remains canonical for translation synchronization, but verify that the English change itself is intentional. Search all references before deleting or renaming a key. Afterward, search for stale variants.

## Module safety

Search root and modules for duplicate keys and relative file-path collisions. A module must not rely on shadowing another owner's FTL file.

## Verification

```powershell
Get-ChildItem Resources/Locale -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
Get-ChildItem Modules -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
git grep -n -E "EXACT_KEY|OLD_KEY|PROPOSED_KEY|FEATURE_PREFIX|UI_CONTROL" -- Resources Modules Content.*
git diff -- "*.ftl"
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --check
```
