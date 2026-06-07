<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

---
applyTo: "Resources/**/*.yml,Resources/**/*.ftl,Resources/**/*.json"
---

For `Resources/` content:

- Load `ss14-ecs-prototypes`, `ss14-localization-strings`, and `ss14-prototypes-locale`.
- Also load `ss14-graphics-generic-visualizer-appearance` when the edit touches `Appearance`, `GenericVisualizer`, RSI state layout, or layer visibility.
- Use the narrowest existing subtree and keep parent prototypes in `base.yml` when starting a new parent tree.
- Run YAML linting for prototype or FTL edits and RSI validation for RSI metadata changes.
