<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Testing matrix

| Change | Minimum meaningful verification |
| --- | --- |
| Shared/server/client C# | affected build, then relevant tests |
| Network or prediction | integration or runtime latency/rejection test |
| Prototype or FTL | Release build plus YAML linter |
| RSI or sprite state | RSI validator and reference search |
| XAML | client/XAML build and runtime layout check |
| Database schema | both provider migrations and upgrade path |
| Packaging | Content.Packaging build and requested package path |
| Documentation/guidance | dedicated validator and link/path checks |

Increase scope when one feature crosses multiple rows.
