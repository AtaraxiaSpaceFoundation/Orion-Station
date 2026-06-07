---
applyTo: "Content.Server/**/*.cs,Modules/*/Content.*.Server*/**/*.cs"
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

Server code owns validation, protected decisions, persistence, and authoritative mutation. Validate every client-originated request.
