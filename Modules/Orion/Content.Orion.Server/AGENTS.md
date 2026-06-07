<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion Server guidance

Server owns authoritative Orion outcomes, validation, persistence coordination, hidden information, anti-abuse checks, and server-only simulation.

Validate all client messages. Re-resolve network entities and re-check state at execution time. Do not trust UI validation, displayed prices, access cached on the client, or client-selected results.

Keep presentation and XAML out of this project. Send limited BUI state or events rather than exposing complete server components.
