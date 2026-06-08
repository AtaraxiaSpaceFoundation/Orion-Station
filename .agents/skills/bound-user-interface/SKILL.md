---
name: bound-user-interface
description: Implement BUI contracts, server validation, localized client windows, lifecycle cleanup, and state refresh.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Bound User Interface

A BUI crosses Shared, Server, Client, and usually Resources.

Shared defines the UI key, serializable intent messages, and minimal state. Messages express requested actions, not trusted results.

Server handlers validate actor, target, distance, access, ownership, cooldown, current state, and submitted values. Protected outcomes are derived server-side. Mutate authority and send refreshed state.

Client code creates the window, binds state, and sends intent messages. Client validation improves UX only.

Every visible string requires English localization and an ordered Russian counterpart. Long-lived windows must refresh localized text on culture changes without discarding authoritative state or duplicating subscriptions.

Handle open, update, close, deletion, range loss, multiple viewers, and reopen behavior.

Use repository ownership rules for edit markers. Do not mark owner-local module or underscore paths.

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
```

Run the existing integration-test project of every changed module.
