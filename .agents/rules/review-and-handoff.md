<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Review and handoff

A completed agent task must leave an evidence-based handoff.

Report:

- what behavior changed and which owner contains it;
- important authority, networking, prediction, persistence, or resource decisions;
- files changed by category;
- exact validation commands and results;
- checks not run and why;
- migration or compatibility risks;
- follow-up work intentionally excluded.

Do not hide uncertainty behind confident wording. Distinguish verified behavior, code inspection, and inference. Avoid style-only review comments unless style obscures a correctness or maintenance problem.

Before committing, inspect `git diff --check`, `git status`, unexpected generated files, and unrelated local changes.
