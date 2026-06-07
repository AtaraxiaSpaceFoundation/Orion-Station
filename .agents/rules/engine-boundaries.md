<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Engine boundaries

Treat `RobustToolbox/` as an escalation boundary.

Before editing engine code:

1. Confirm the behavior cannot be implemented in root content or an owning module.
2. Search for an existing public engine hook or content-side extension point.
3. Check whether a small content API addition can avoid an engine modification.
4. Explain why the engine change is required.

Good engine changes provide a generally reusable primitive that content cannot express. Bad engine changes move fork-specific gameplay into the engine, bypass existing APIs, or combine a small bug fix with unrelated engine refactoring.

Keep engine diffs narrow. Preserve upstream style and avoid changing engine internals merely because the content-side solution looks less elegant. When the change should be upstreamed, keep fork-specific references out of the implementation.
