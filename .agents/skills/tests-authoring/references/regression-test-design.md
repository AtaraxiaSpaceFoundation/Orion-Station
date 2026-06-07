<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Regression test design

A regression test should contain the smallest setup that reproduces the old failure and an assertion that distinguishes fixed from broken behavior.

Good additional cases include:

- lower and upper boundaries;
- repeated call;
- rejected permission;
- entity deletion;
- cancellation;
- reconnect or late state;
- invalid prototype reference;
- stale client state;
- provider-specific database behavior.
