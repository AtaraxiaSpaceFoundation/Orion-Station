---
name: testing
description: Select and write focused tests, integration tests, YAML linting, RSI validation, and packaging checks.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Testing

Match the test to the failure mode.

## Focused tests

Use `Content.Tests` for isolated root-content systems, validation, serialization, prototypes, and deterministic regressions.

## Integration tests

Use `Content.IntegrationTests` when integrated root-content server/client state, networking, maps, multiple systems, or lifecycle matters. Use `Modules/<Module>/Content.<Module>.IntegrationTests` for integrated behavior owned by a specific module.

## Repository commands

- `dotnet restore`
- `dotnet build --configuration Debug --no-restore /m`
- `dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0`
- `dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed`
- `dotnet test --no-build --configuration Debug Modules/Orion/Content.Orion.IntegrationTests/Content.Orion.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed`
- build `Release`, then run `Content.YAMLLinter` with `--no-build`
- run RSI validation for sprite changes
- build and run `Content.Packaging` when packaging is affected

Keep tests deterministic. Report every command actually run.
