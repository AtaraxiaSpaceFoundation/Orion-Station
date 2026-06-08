<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Content and Localization

Store content in the resource root declared by its owner. Base resources belong in root `Resources`. Module resources belong in that module.

## Player-visible text

All player-visible text MUST use localization. Do not expose raw enum names, prototype IDs, component names, exception messages, or internal validation details.

## Discover before placement

Before editing localization:

1. inspect the complete owner-local `en-US` and `ru-RU` trees
2. search exact, old, alternative, and proposed keys
3. identify the existing file for the same UI, system, prototype family, or feature
4. inspect the mirrored file in the other culture
5. create a file only when no existing file is a valid owner

Preserve exact directories, leading underscores, casing, grouping, and relative paths. Never derive a path from a module name without inspecting the repository.

## English is structurally canonical

`en-US` defines:

- the key set
- key names
- message order
- section order
- required attributes
- variable names
- selector structure
- relative file paths

Any structural English change MUST be mirrored in `ru-RU`.

New Russian entries go at the corresponding English position. They MUST NOT be appended to the end by default.

Removing, renaming, moving, or reordering English localization requires the same Russian operation. Search code, XAML, prototypes, maps, and locale files for stale references after renames or removals.

A Russian-only wording fix may remain Russian-only when the structural contract is unchanged.

Russian-only keys are presumed stale unless a verified framework requirement or documented convention proves otherwise.

## Translation quality

Write natural Russian for the actual context. Preserve meaning, variables, markup, and control hints rather than English word order.

Do not add `THE(...)`, article wrappers, or equivalent English grammar markers to Russian strings.

Check long Russian text in constrained UI controls. Fix layout rather than silently removing meaning.

## Module paths and duplicates

Before creating a module FTL file, search root resources and every module for the same relative path. A module MUST NOT depend on shadowing a core or foreign-module file.

Search keys globally. File separation does not make duplicate FTL keys safe.

## Runtime culture behavior

Do not store localized output in persistent, network, or authoritative state. Resolve text at the presentation boundary. Long-lived controls must refresh on culture changes and clean up subscriptions.

## Verification

```powershell
Get-ChildItem Resources/Locale -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
Get-ChildItem Modules -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
git grep -n -E "EXACT_KEY|OLD_KEY|PROPOSED_KEY|FEATURE_PREFIX" -- Resources Modules Content.*
git diff -- "*.ftl"
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --check
```
