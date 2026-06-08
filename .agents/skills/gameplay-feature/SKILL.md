---
name: gameplay-feature
description: Implement complete SS14 features across ownership, ECS, networking, resources, localization, UI, and tests.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Gameplay Feature

Use this as the routing skill for multi-layer work.

Before editing, map repository ownership, owner-local paths, inherited files, edit-marker requirements, assembly layers, authority, prototypes, resources, English and Russian locale files, UI lifecycle, persistence, and tests.

A feature is incomplete when code exists but prototypes, ordered Russian localization, assets, UI refresh, validation, or owner tests are missing.

## Implementation order

1. Verify repository identity, APIs, ownership, and extension points.
2. Define data and contracts in the lowest valid assembly.
3. Implement server authority and validation.
4. Implement client presentation and prediction-safe feedback.
5. Add prototypes and resources in the owner root.
6. Add English localization and mirror Russian structure and order.
7. Add regression coverage in existing owner tests.
8. Run commands for every changed surface.

Mark inherited edits with the current repository marker. Do not add redundant markers in owner-local module or underscore paths.

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
```

Also run the existing integration-test project for every changed module.
