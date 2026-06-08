---
name: forms-and-input-validation
description: Validate UI, command, text, numeric, entity, and configuration input with localized feedback and server authority.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Forms And Input Validation

Validation has two layers: client feedback and authoritative server enforcement.

## Parsing

Trim deliberately. Parse numbers with an explicit culture and format policy. Reject ambiguous forms such as integer fields supplied as `1.0` unless the domain permits them. Check length before expensive processing.

## Domain validation

Validate ranges, overflow, whitelists, prototype IDs, entity existence, ownership, access, cooldowns, and cross-field constraints. Normalize case and whitespace only when the domain defines them as insignificant.

Use one authoritative validation source where possible. Do not allow UI and server rules to drift.

## Feedback

Return specific player-safe errors in both `en-US` and `ru-RU`. Do not expose hidden state, internal exceptions, raw IDs, or server-only reasons.

Variable names and values passed into validation messages must match both locale files.

## Security failures

Treat rich text, markup, paths, URLs, and command fragments as hostile. Use structured APIs instead of concatenating shell, SQL, markup, or resource paths.

## Verification commands

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0 NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```

Run the owning integration project for BUI, command authority, stale entities, culture-sensitive UI, or client/server behavior. Test empty, whitespace, boundaries, overflow, locale variants, malformed markup, unauthorized actors, and conflicting fields.
