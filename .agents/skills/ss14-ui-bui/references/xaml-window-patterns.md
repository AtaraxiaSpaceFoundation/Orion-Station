<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

<!-- SPDX-License-Identifier: LicenseRef-OpenSpace-AgentPrompts-Restricted -->

# XAML Window Patterns

## Rules

- prefer XAML for new windows and controls
- keep `.xaml` and `.xaml.cs` paired
- reuse nearby windows and `FancyWindow` patterns before creating new layout structures
- localize titles, labels, buttons, and tooltips

## Reminder

Do not edit only the XAML without checking the shared and server-side state the window depends on.
