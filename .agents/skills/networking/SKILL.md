---
name: networking
description: Implement minimal network events and component state with authority, conversion, dirtying, and regression coverage.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Networking

Define payloads in Shared and verify current serialization and entity-conversion APIs. Keep payloads minimal. Messages express intent, while the server derives protected results.

Replicate only fields needed by clients. After authoritative mutation, call the correct dirtying path. Predicted state must converge without duplicate popups, sounds, spawns, or resource charges.

Player-facing feedback generated from replicated state requires English localization and an ordered Russian counterpart.

Verify repository ownership and edit-marker requirements before modifying inherited files.

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```

Run the existing integration-test project for every changed module.
