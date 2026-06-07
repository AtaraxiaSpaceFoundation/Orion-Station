<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion Client guidance

Client owns Orion presentation, visual systems, controls, XAML, code-behind, local input presentation, and client BUI/EUI classes.

Do not make protected decisions or persist authoritative state on the client. Treat replicated data and server messages as inputs to presentation. Avoid duplicate predicted effects and clean up subscriptions, overlays, and UI lifetimes.

Keep client-only dependencies out of Shared.
