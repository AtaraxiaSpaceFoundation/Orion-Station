<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# BUI flow

1. Interaction requests the UI.
2. Server verifies the user may open it.
3. Server sends a minimal state object.
4. Client renders state.
5. User action sends a shared intent message.
6. Server validates again and performs the action.
7. Server sends new state or closes the UI.

Never treat step 4 or client-side button enablement as authorization.
