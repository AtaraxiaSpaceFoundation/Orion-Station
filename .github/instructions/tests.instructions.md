---
applyTo: "Content.Tests/**/*.cs,Content.IntegrationTests/**/*.cs,Modules/*/Content.*.IntegrationTests/**/*.cs"
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

Root `AGENTS.md` hard rules apply. Read the nearest scoped `AGENTS.md`, `.agents/skills/testing/SKILL.md`, `.agents/skills/tests-authoring/SKILL.md`, and the relevant domain skill.

Verify repository identity, owner tag, owner-local module and underscore paths, and edit-marker requirements before changing inherited test files. Do not add redundant owner markers inside owner-local paths.

Use the existing owner test project. Do not create duplicate integration projects, PoolManager fixtures, CI steps, MSBuild targets, or `module.yml` entries. Verify declarations, access modifiers, assemblies, and project references.

For locale or culture tests, treat `en-US` as structural truth. Verify `ru-RU` message IDs, attributes, variables, selectors, relative file paths, ordered positions, fallback, repeated switching, persistence, and subscription cleanup.

Use exact commands and NUnit arguments from the canonical testing skill. Never edit SPDX metadata or weaken assertions to obtain a pass.
