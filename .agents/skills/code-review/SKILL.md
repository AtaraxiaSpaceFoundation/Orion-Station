---
name: code-review
description: Review correctness, access, repository ownership, authority, localization, compatibility, resources, scope, and verification evidence.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Code Review

Review observable behavior and architectural validity before style.

1. Define intended behavior and scope.
2. Verify repository identity, owner tag, owner-local paths, and upstream.
3. Verify edit markers on inherited files and the absence of redundant markers in owner-local module and underscore paths.
4. Identify projects, assemblies, declarations, and access modifiers.
5. Trace validation, mutation, dirtying, persistence, lifecycle, and presentation.
6. Check failure paths, cleanup, repeated execution, and reconnect.
7. Compare English and Russian keys, attributes, variables, selectors, relative paths, and message order.
8. Verify resources, compatibility surfaces, test ownership, and regression behavior.
9. Inspect the complete diff for unrelated changes, SPDX edits, and duplicate infrastructure.
10. Review maintainability and style last.

Treat inaccessible members, cross-assembly partial assumptions, reversed dependencies, duplicate infrastructure, foreign edit markers, missing inherited-file markers, redundant owner-local markers, missing Russian entries, Russian order drift, mismatched FTL contracts, stale culture UI, hardcoded text, and unproven test claims as defects.

Every finding needs a path, declaration or execution sequence, impact, and minimal remediation.
