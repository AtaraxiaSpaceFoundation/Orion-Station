<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# System and event patterns

A typical action flow is:

`OnEvent -> TryAction -> CanAction -> ExecuteAction`

The event handler handles routing flags and delegates. `TryAction` is the public reusable API and performs its own checks. `CanAction` does not mutate state. Execution applies all state changes once.

If multiple callers need detailed failure information, use a small result type only when nearby code follows that pattern. Do not replace a simple boolean workflow with an elaborate abstraction without evidence.
