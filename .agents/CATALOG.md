<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Agent Guidance Catalog

Read root `AGENTS.md` and the nearest scoped `AGENTS.md` first. Select only skills required by changed surfaces.

Every selected skill is an execution contract. Follow its discovery, ownership, edit-marker, localization, stop, and verification rules.

## Mandatory routing

- Repository identity, inherited files, modules, or underscore owner paths: `module-architecture`, `upstream-maintenance`, `git-workflow`.
- C# or project references: `csharp-style`, `module-architecture`, `testing`.
- Shared, networking, prediction, or authority: `client-server-shared`, `networking`, `prediction`, `tests-authoring`.
- FTL or player-visible text: `localization`, `localization-in-code`, `testing`.
- Prototypes with display text: `prototypes`, `prototype-localization`, `localization`, `yaml-and-schema`.
- XAML or BUI: `xaml-ui`, `bound-user-interface`, `forms-and-input-validation`, `localization`, `testing`.
- Assets or maps: `resources-and-assets`, `maps-and-mapping`, `yaml-and-schema`, `testing`.
- Project, solution, CI, MSBuild, or packaging: `module-architecture`, `build-and-packaging`, `testing`.
- Ports: `porting`, `upstream-maintenance`, and every domain skill matching ported files.

`en-US` is the structural localization source of truth. Any localization task must preserve Russian key, attribute, variable, selector, file-path, and message-order parity.
