<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# ECS reading guide

Search by component type, event type, prototype ID, and user-visible locale key. These searches reveal different parts of the same feature.

Build a small flow map:

`prototype -> component -> subscription -> validation -> mutation -> dirtying -> client visual/UI -> tests`

Mark each arrow with its assembly. Unexpected Server-to-Client shortcuts or Shared references to client-only types usually indicate an ownership problem.
