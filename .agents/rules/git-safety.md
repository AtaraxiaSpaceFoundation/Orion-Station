<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Git safety

Inspect `git status --short --branch` before staging or committing.

Do not run destructive commands without explicit approval for that exact action:

- `git reset --hard`;
- `git clean -fd` or `git clean -fdx`;
- `git checkout -- .`;
- `git restore .`;
- force push or history rewriting;
- recursive forced deletion;
- worktree deletion;
- removal of untracked files.

Keep unrelated local files out of commits. Do not silently amend, rebase, squash, or force-update a shared branch. When a push fails because the remote moved, report it instead of choosing a merge or rebase policy for the user.

Read-only commands such as status, diff, log, show, grep, and ls-files are safe defaults.
