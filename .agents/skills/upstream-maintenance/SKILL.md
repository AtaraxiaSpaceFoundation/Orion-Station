---
name: upstream-maintenance
description: Keep inherited changes narrow, correctly marked, traceable, and easy to rebase across repository layers.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Upstream Maintenance

Before editing inherited code, determine whether the change is an upstream bug fix, reusable extension point, compatibility adaptation, or repository-owned behavior.

Verify the current repository owner tag and existing marker syntax.

Repository-owned behavior belongs in owner-local module or underscore paths whenever possible. Those paths do not receive redundant owner edit markers.

When an inherited file must change:

- add the current repository marker around the smallest changed block
- preserve nearby formatting and upstream layout
- avoid unrelated cleanup
- document intentional divergence and source revision when relevant
- never replace an existing foreign marker with the current marker without understanding ownership

Separate mechanical upstream conflict resolution from repository behavior changes. Do not copy a whole inherited file merely to change a small branch when a supported extension point exists.
