---
name: git-workflow
description: Inspect, stage, commit, rebase, merge, and deliver changes without losing user work or polluting scope.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Git Workflow

Start every write task with `git status --short --branch` and inspect the branch relationship to its remote.

## Scope control

Stage only task-related paths. Inspect staged diff before committing. Keep generated, untracked, or user-owned files out unless explicitly requested.

## Shared history

Do not force-push, reset hard, clean untracked files, rewrite published history, or choose merge/rebase policy without approval. When the remote moved, show the divergence and ask or follow explicit repository policy.

## Commits

Create logical commits with messages matching repository conventions. Do not claim a commit was pushed until the remote ref is verified. A task record or local commit is not a GitHub update.

## Conflict handling

Understand both sides before resolving. Preserve current architecture and intended behavior rather than selecting one side mechanically. Run relevant checks after conflict resolution.

## Delivery

Verify the target branch SHA and existing PR head after push. Do not create a new PR when the user asked to update an existing branch.
