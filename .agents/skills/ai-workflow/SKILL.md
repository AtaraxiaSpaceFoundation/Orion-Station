---
name: ai-workflow
description: Plan, verify, implement, and deliver repository work through evidence-backed ownership and quality gates.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# AI Workflow

## Establish the contract

Resolve repository identity, owner tag, upstream, branch, requested outcome, exclusions, commit count, allowed paths, and required checks.

Record initial worktree state and existing SPDX diff.

## Complete the preflight

```text
Repository and owner tag:
Owner module and underscore paths:
Inherited paths and marker syntax:
Target and caller assemblies:
Required declarations and access:
Existing extension point:
Existing test owner:
Localization owner and mirrored files:
Verification plan:
```

No implementation starts while a correctness-critical field is unknown.

## Explore before designing

Map entry points, dependencies, resources, tests, lifecycle, localization order, and current behavior using exact paths and symbols.

Subagent summaries are not proof without declarations, file evidence, or command output.

## Implement narrowly

Reuse existing owners and infrastructure. Mark inherited edits with the current repository marker. Do not mark owner-local module or underscore paths. Do not edit SPDX.

Treat English localization as the structural source of truth and mirror every structural change in Russian at the same relative position.

## Verify claims

Run the narrowest meaningful check first, then broaden according to risk. A missing tool is a limitation, not a pass.

## Deliver

Inspect final status and diff. Confirm scope, marker placement, accessibility, localization order parity, absence of duplicate infrastructure, exact command results, commit count, and remote branch SHA.
