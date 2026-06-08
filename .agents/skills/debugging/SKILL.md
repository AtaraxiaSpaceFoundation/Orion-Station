---
name: debugging
description: Reproduce failures, trace authoritative flow, prove the broken assumption, and add targeted regression coverage.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Debugging

Debug before redesigning.

1. Reproduce the exact symptom and record inputs, runtime side, repository owner, and lifecycle stage.
2. Identify the authoritative owner and public entry point.
3. Trace subscription, validation, mutation, dirtying, persistence, and presentation.
4. Compare server and client state for networking issues.
5. Verify every assumed declaration and resource reference.
6. Add temporary diagnostics only around the suspected divergence.
7. Confirm the failing assumption, fix the narrow cause, remove diagnostics, and add a stable regression test.

For localization bugs, compare English and Russian file discovery, relative paths, ordered keys, variables, selectors, loaded roots, and UI refresh.

Apply repository markers only to inherited files and never to owner-local module or underscore paths.

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0 NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```

Run the existing integration-test project for each affected owner.
