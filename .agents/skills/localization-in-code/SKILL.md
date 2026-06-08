---
name: localization-in-code
description: Resolve localized text at presentation boundaries after verifying English-source keys, owner files, ordered Russian parity, and lifecycle refresh.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Localization In Code

Keep internal logic typed and language-independent. Resolve localized text only where presented.

Before adding `LocId`, `Loc.GetString`, XAML localization, popup text, or validation text:

1. search the exact English key and competing spellings
2. inspect neighboring English keys used by the same feature
3. locate the existing English file and mirrored Russian file
4. preserve their relative path and message order
5. add the Russian entry at the corresponding English position

English defines message IDs, required attributes, variables, selectors, and ordering. Russian mirrors the contract while using natural language.

Do not store resolved localized strings in components, persistence, network state, equality checks, or durable caches.

If UI survives culture changes, refresh labels, tooltips, placeholders, and generated rows through the supported lifecycle. Subscribe once and unsubscribe on disposal or shutdown.

Pass typed values, entities, counts, durations, and prototype-derived text as variables. Variable names and selector input types must match English and Russian.

Use entity-name and grammar helpers instead of manual name, pronoun, or article concatenation. Russian output must not receive `THE(...)` or equivalent English grammar wrappers.

Review failures include hardcoded player text, keys absent from Russian, Russian keys appended out of English order, mismatched variables, localized string comparison, persisted output, stale UI, and duplicate culture-change subscriptions.

```powershell
Get-ChildItem Resources/Locale -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
Get-ChildItem Modules -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
git grep -n -E "EXACT_KEY|OLD_KEY|PROPOSED_KEY|UI_CONTROL" -- Resources Modules Content.*
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
```
