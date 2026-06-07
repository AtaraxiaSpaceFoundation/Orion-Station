# Verification

Select verification from the files and behavior changed.

Repository baseline:

- SDK comes from `global.json` and is currently .NET 10.
- Restore with `dotnet restore`.
- CI builds and tests in `Debug`.
- `Content.Tests` covers focused content tests.
- `Content.IntegrationTests` covers integrated client/server behavior.
- YAML linting builds in `Release`, then runs the linter with `--no-build`.
- RSI changes require the RobustToolbox RSI validator.
- Packaging changes require `Content.Packaging`.

Use the smallest meaningful check first, then broaden when the change crosses assemblies or resources. A documentation-only change does not require claiming a full game build. A networking change should not be considered verified by YAML parsing alone.

Report exact commands, their results, and checks that could not be run. Never convert an unavailable tool into an implied success.
