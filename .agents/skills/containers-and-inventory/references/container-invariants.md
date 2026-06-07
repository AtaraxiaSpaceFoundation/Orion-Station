<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Container invariants

At any point, a contained entity should have one consistent owner, parent relationship, container registration, and network visibility state.

Check invariants after insertion, removal, transfer, deletion, map transfer, and body replacement. Avoid keeping cached `EntityUid` references to inventory content without deletion handling.

When an operation moves several items, define whether it is atomic, best-effort, or rollback-capable.
