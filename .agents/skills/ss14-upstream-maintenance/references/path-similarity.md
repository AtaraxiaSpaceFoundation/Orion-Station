<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# Path Similarity

## Rule

When creating fork/module-specific files, mirror the existing feature path as closely as practical.

## Example

- upstream-like behavior under `Content.Shared/<Feature>/...`
- Orion-side extension under `Content.Shared/_Orion/<Feature>/...` when the Orion split is warranted, or the matching inherited subtree when that feature already has one

## Why

- keeps drift discoverable
- makes upstream rebases easier
- helps humans and agents find the fork/module delta quickly
