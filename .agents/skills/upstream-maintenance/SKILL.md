---
name: upstream-maintenance
description: Keep inherited changes narrow, traceable, and easy to compare with Goob Reforged or other owners.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Upstream Maintenance

Before editing inherited code, determine whether the change is:

- an upstream bug fix;
- a reusable extension point;
- a compatibility adaptation;
- Orion-only behavior.

Keep Orion-only implementation in Orion. If an inherited project needs a hook, add the narrowest reusable hook and avoid unrelated formatting or refactoring.

Preserve file layout and nearby style to reduce future conflicts. Document intentional divergence and the upstream source revision. When updating upstream, separate mechanical conflict resolution from Orion behavior changes.

Do not copy an entire source file merely to change a small branch when a partial extension is possible.
