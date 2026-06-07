<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Base Server guidance

Root Server is authoritative base content. Changes here have broad impact.

Validate client actions, preserve persistence and security boundaries, and avoid module-specific branches. If only Orion needs the behavior, implement it in `Modules/Orion/Content.Orion.Server` or add a narrow reusable extension point here.
