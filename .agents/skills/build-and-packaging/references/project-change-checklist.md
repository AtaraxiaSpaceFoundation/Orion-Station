<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Project change checklist

- project belongs to the correct module and solution group;
- root namespace and assembly name match repository conventions;
- references obey Common/Shared/Server/Client direction;
- module resources are loaded through the supported mechanism;
- XAML or generated-code targets are configured when required;
- Debug and Release both restore/build where relevant;
- packaging contains required files exactly once;
- CI uses the same commands documented in guidance.
