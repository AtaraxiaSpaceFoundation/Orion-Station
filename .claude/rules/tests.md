<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Tests adapter

Root `AGENTS.md` hard rules apply. Read the nearest scoped `AGENTS.md` and canonical `testing` and `tests-authoring` skills.

Verify repository identity, owner tag, owner-local module and underscore paths, and edit-marker requirements before changing inherited test files. Do not add redundant owner markers inside owner-local paths.

Use existing owner-local infrastructure. Verify declarations, access modifiers, assemblies, runtime sides, and project references before writing test code.

Do not create duplicate test projects, PoolManager fixtures, CI steps, MSBuild targets, or `module.yml` entries. Never edit SPDX metadata or weaken assertions.

For localization and culture behavior, treat `en-US` as structural truth. Verify `ru-RU` message IDs, attributes, variables, selectors, relative file paths, ordered positions, fallback, persistence, repeated switching, and subscription cleanup.

Run exact canonical commands and preserve complete failure output.
