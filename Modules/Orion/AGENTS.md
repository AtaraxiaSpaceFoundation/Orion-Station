<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion module guidance

`Modules/Orion` owns Orion-specific code and resources.

- Common may depend on root `Content.Common`, but not Orion Shared, Server, or Client.
- Shared may depend on Orion Common and root `Content.Shared`.
- Server may depend on Orion Common, Orion Shared, and root `Content.Server`.
- Client may depend on Orion Common, Orion Shared, and root `Content.Client`.
- Orion resources belong in `Modules/Orion/Resources`.

Prefer module-local systems, components, prototypes, locale, UI, and assets. Change root content or GoobStation only when a reusable extension point or inherited fix is truly required. Explain cross-owner edits in the PR.

For ports from the old Orion repository, map the complete feature family into this module and separately review every source modification that touched base or Goob code.
