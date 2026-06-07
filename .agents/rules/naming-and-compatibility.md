<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Naming and compatibility

Follow established repository vocabulary before inventing a new term.

- C# types use PascalCase, members follow nearby style, and private fields use the repository convention.
- Components end with `Component`; systems end with `System` unless a framework base defines another established pattern.
- Events describe timing and direction clearly, such as `Before`, `Attempt`, `Changed`, or `Completed`.
- Prototype IDs are stable API-like identifiers. Do not rename them casually.
- FTL keys use lowercase kebab-case with a feature-scoped prefix.
- Resource paths and sprite states use stable lowercase naming consistent with nearby assets.

Treat serialized field names, prototype IDs, network contracts, database columns, map entities, and saved configuration as compatibility surfaces. Renames require migration, aliases, or an explicit compatibility decision.
