<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Integration Test Guidance

This directory owns base-content integration tests and shared integration-test infrastructure. Module-specific behavior belongs in the corresponding existing module integration project.

Use integration tests for behavior crossing server, client, networking, maps, prototypes, culture lifecycle, UI lifecycle, or multiple systems.

Control time and randomness. Assert authority first and replicated client state when relevant. Keep setup minimal and clean up entities, maps, sessions, subscriptions, configuration, and global state.

Do not duplicate PoolManager setup in module projects. Do not use runtime module loading as compile-time access. Do not add module test projects to `module.yml`.

For localization behavior, treat English as structural truth and verify ordered Russian parity, variables, selectors, fallback, repeated switching, and subscription cleanup.

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```
