---
name: build-and-packaging
description: Change projects, solution structure, CI, generated content, and packaging with repository-correct ownership and exact commands.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Build And Packaging

Use this for project files, solution structure, MSBuild targets, workflows, module manifests, packaging rules, and generated outputs.

Verify repository identity, owner tag, owner-local module and underscore paths, assembly role, dependency direction, target framework, XAML imports, generated-code targets, solution grouping, and manifest role.

Do not introduce base-to-module references. Do not make Shared depend on Client or Server. Do not add integration-test projects to `module.yml`. Search for existing projects and CI steps before creating anything.

Inherited workflow or project changes require the current repository edit marker when the file format supports comments. Owner-local module and underscore paths do not receive redundant owner markers.

Copy commands and flags from the current repository. Do not import commands from another fork. Preserve submodule initialization, output paths, test arguments, and artifacts.

```powershell
git submodule update --init --recursive
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0 NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
dotnet build Content.Packaging --configuration Release --no-restore /m
```

Discover and run each affected module integration project. Run packaging platforms and specialized validators from exact current workflows. Report unavailable checks explicitly.
