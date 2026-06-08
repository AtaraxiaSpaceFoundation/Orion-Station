<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion integration-test guidance

This project owns integrated client/server tests for Orion behavior. Reference `Content.Benchmarks` and only the Orion Client, Server, Shared, or Common projects required at compile time; runtime-loaded modules do not grant compile-time access to their types.

Use `GameTest` and `SidedDependency` for real server/client environments. Assert authoritative server outcomes first and replicated client state when it is part of the contract. Use simulation ticks and pair helpers instead of wall-clock sleeps. Every test must contain observable assertions, avoid test-order dependencies, and clean up any maps, entities, sessions, or global state it creates beyond `GameTest` cleanup.

Run locally with:

```sh
dotnet test --no-build --configuration Debug Modules/Orion/Content.Orion.IntegrationTests/Content.Orion.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed
```

If this project is renamed or moved, update its `SpaceStation14.slnx` entry and dedicated CI step together.
