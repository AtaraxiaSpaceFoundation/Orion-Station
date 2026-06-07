---
name: documentation
description: Write concise technical documentation, PR notes, architecture explanations, and verification reports.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Documentation

Documentation should explain decisions that code alone does not make obvious.

For a substantial change, cover:

- problem and intended behavior;
- owner and assembly boundaries;
- important data or event flow;
- compatibility and migration concerns;
- resources and localization;
- tests performed and not performed.

Prefer updating an existing canonical document over adding an isolated note. Use real paths and commands. Do not claim current behavior based on the old repository.

Keep player changelogs separate from internal implementation notes.
