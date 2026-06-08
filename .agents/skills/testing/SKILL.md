---
name: testing
description: Select the existing owner test project and run checks matching the real failure mode.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Testing

Match tests to the owner and failure mode.

Use `Content.Tests` for focused root tests, `Content.IntegrationTests` for integrated root behavior, and the existing `Modules/<Module>/Content.<Module>.IntegrationTests` project for module behavior.

Search before creating a project, fixture, CI step, or MSBuild target. Duplicate infrastructure is forbidden. Test projects do not belong in `module.yml`.

Test through accessible public APIs or real event paths. Do not use reflection to reach private implementation.

For localization changes, verify that Russian mirrors English message IDs, attributes, variables, selectors, relative paths, and message order.

Run restore, the Debug build, the applicable root tests, and every affected module integration project. Report exact commands, failures, and omitted checks.
