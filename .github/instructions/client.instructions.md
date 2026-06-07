---
applyTo: "Content.Client/**/*.cs,Modules/*/Content.*.Client*/**/*.cs,**/*.xaml"
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

Client code owns presentation, visuals, controls, and UI. Do not move protected decisions client-side. Review UI lifetime and predicted duplicate effects.
