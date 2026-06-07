<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Interactions and authority

Every player-triggered operation needs an explicit authority path.

- Client checks provide immediate feedback, not security.
- Shared code may predict deterministic, reversible feedback.
- Server code validates the current actor, target, range, access, ownership, resources, cooldown, and state.
- Mutations happen once in the authoritative execution path.
- Replicated changes are dirtied and UI state is refreshed.

Prefer `On... -> Try... -> Can... -> Execute...`. `Can...` must not mutate. `Try...` must not assume the caller already validated. UI messages, actions, verbs, interactions, and network events all re-enter server validation.

When an action is interruptible, model cancellation and completion explicitly. Do not charge resources, award outcomes, or persist state before the authoritative completion point unless rollback is defined.
