---
name: tests-authoring
description: Author deterministic owner-correct tests that prove observable contracts through valid APIs.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Tests Authoring

A useful test fails for the original defect and proves a caller-visible contract.

Place focused root tests in `Content.Tests`, root integration tests in `Content.IntegrationTests`, and module integration tests in the existing `Modules/<Module>/Content.<Module>.IntegrationTests` project.

Do not create duplicate projects, fixtures, CI steps, MSBuild targets, or `module.yml` entries.

Act through the public API, real event, command, UI message, or lifecycle entry point used by production. Verify declarations, access modifiers, assemblies, runtime sides, and project references before using symbols.

Use controlled simulation time and deterministic randomness. Avoid wall-clock sleeps, machine locale, live services, order dependence, and leaked global state.

For localization behavior, test English and Russian key parity, ordered placement, variables, selectors, fallback, repeated switching, and subscription cleanup.

Run the Debug build, the complete owning test project, and every affected module integration project. Preserve complete failure output and report exact filters and arguments.
