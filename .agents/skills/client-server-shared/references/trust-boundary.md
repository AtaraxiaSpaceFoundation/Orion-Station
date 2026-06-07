<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Trust boundary checklist

Treat every network message as untrusted input.

Re-resolve entities on receipt. Check actor ownership, target validity, range, access, state, quantity, cooldown, and current authoritative values.

Never trust a price, balance, permission, success flag, random result, or account identity supplied by the client when the server can derive it.

Send a limited response or BUI state rather than replicating the complete authoritative component.
