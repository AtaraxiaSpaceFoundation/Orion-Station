<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Content and localization

Store content in the owning resource root declared by `module.yml`. Base resources belong in root `Resources`; module resources belong in that module.

Player-visible strings must use localization. This includes popups, chat messages, examine text, UI labels, tooltips, prototype names, descriptions, marking names, and operator-facing client text.

Use specific, feature-scoped FTL keys. Pass variables explicitly. Keep reusable locale keys as `LocId` where nearby serialized data uses that type. Do not compare localized strings in logic and do not expose raw enum names or prototype IDs to players.

For Russian localization, write natural Russian text and do not introduce wrappers such as `THE(...)` or equivalent article markers.

Whenever code references a prototype, sprite state, audio path, or locale key, verify that the referenced resource exists in the owning resource root. Preserve asset attribution and asset-specific license metadata.
