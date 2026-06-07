---
name: testing
description: Select and write focused tests, integration tests, YAML linting, RSI validation, and packaging checks.
---

# Testing

Match the test to the failure mode.

## Focused tests

Use `Content.Tests` for isolated systems, validation, serialization, prototypes, and deterministic regressions.

## Integration tests

Use `Content.IntegrationTests` when server/client state, networking, maps, multiple systems, or lifecycle integration matters.

## Repository commands

- `dotnet restore`
- `dotnet build --configuration Debug --no-restore /m`
- `dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0`
- `dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed`
- build `Release`, then run `Content.YAMLLinter` with `--no-build`
- run RSI validation for sprite changes
- build and run `Content.Packaging` when packaging is affected

Keep tests deterministic. Report every command actually run.
