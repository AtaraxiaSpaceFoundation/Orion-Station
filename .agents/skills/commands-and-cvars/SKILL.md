---
name: commands-and-cvars
description: Implement commands and CVar-backed configuration with validation, authority, persistence, localized feedback, and compatibility.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Commands And Cvars

## Console commands

Keep parsing, authorization, validation, execution, and feedback distinct. Verify the current command and shell interfaces before implementation.

Commands affecting players, round state, persistence, or server configuration require permission checks and useful audit logging.

Command descriptions, help, usage, success, and player-safe errors must use `en-US` and `ru-RU` where the command framework supports localization. Do not expose internal exceptions or raw IDs.

## CVars

Use the current typed CVar definition pattern. Choose a stable name, safe default, correct flags, documented unit, and explicit runtime behavior.

Decide whether the value is server-only, replicated, archived, startup-only, or runtime-changeable. Validate bounds before applying timers, rates, sizes, economy values, or paths.

Subscribe only when live changes are supported. Unsubscribe during system shutdown. Persisted client settings must survive restart and handle removed or invalid values safely.

CVar and command names are operational APIs. Renames require migration or compatibility handling.

## Verification commands

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0 NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```

Run the owning integration project for replicated settings, client UI, permission flows, runtime changes, or restart lifecycle. Test missing arguments, invalid and boundary values, unauthorized use, config reload, help output, and persisted fallback.
