---
name: documentation
description: Write concise technical guidance, PR notes, architecture explanations, and reproducible verification reports.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Documentation

Documentation should explain decisions that code alone does not make obvious.

For substantial changes, document the problem, intended behavior, owner, assembly boundaries, data or event flow, compatibility, `en-US` and `ru-RU` impact, resources, tests, and limitations.

Use current paths, declarations, and exact commands. Do not claim behavior based on the old repository or a source fork.

Verification reports must separate:

- commands actually run and their result;
- commands not run and why;
- manual checks performed;
- unresolved risks;
- files requiring human SPDX metadata.

Do not write “all tests passed” when only a build ran. Do not translate internal implementation notes into a player changelog. Do not add documentation files when an existing canonical file owns the information.

## Verification commands

```powershell
git diff --name-status
git diff --check
git diff -U0
```

When documenting commands, copy them from current workflows or verified project instructions. Do not invent or simplify flags.
