<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion module guidance

`Modules/Orion` owns Orion-specific code and resources. Root hard rules remain in force.

## Owner-local paths and edit markers

Treat `Modules/Orion`, verified `_Orion` paths, and Orion-owned projects as owner-local.

Do not add `// Orion-Edit`, `# Orion-Edit`, or equivalent Orion markers inside owner-local paths. These markers are only for Orion changes to inherited files outside Orion-owned paths.

When an inherited file must change, use the existing repository marker syntax around the smallest changed block.

## Dependency direction

- Common may depend on root `Content.Common`, but not Orion Shared, Server, or Client.
- Shared may depend on Orion Common and root `Content.Shared`.
- Server may depend on Orion Common, Orion Shared, and root `Content.Server`.
- Client may depend on Orion Common, Orion Shared, and root `Content.Client`.
- Orion resources belong in `Modules/Orion/Resources`.

Do not add root-to-Orion dependencies or Orion-to-foreign-module references for convenience.

## Core access

Orion projects are separate assemblies from root Content. Verify declarations, modifiers, assemblies, and project references before using root symbols.

A matching namespace, extension method, or partial declaration does not make Orion code part of core. If Orion requires inaccessible core state, STOP and identify the smallest reusable core hook.

## Localization structure

New Orion player-visible text requires both `en-US` and `ru-RU` unless explicitly scoped otherwise.

`en-US` is the structural source of truth. When English adds, removes, renames, moves, or reorders a message, attribute, variable, selector, section, or file, apply the same structural change to Russian.

Insert new Russian messages at the corresponding English position. Do not append them to the end unless the English entry is also at the end.

Before selecting a file, inspect both locale trees, search exact and competing keys, identify the existing feature owner file, and inspect its mirrored counterpart.

The established module-local locale namespace directory is `_orion`. Do not create `orion` or derive a new hierarchy from the module name.

Preserve exact paths, underscores, casing, attributes, variables, selectors, and ordering. Russian must be natural and must not contain `THE(...)` wrappers.

## Existing infrastructure

Prefer Orion-local systems, components, prototypes, locale, UI, assets, and the existing `Content.Orion.IntegrationTests` project.

`Content.Orion.IntegrationTests` is not a runtime project and MUST NOT be listed in `module.yml`. Do not create another Orion integration project, duplicate PoolManager setup, or duplicate its CI step.

## Verification

Use the existing Orion project build and integration-test commands from current workflows. When prototypes, locale, maps, or structured resources change, also run the Release build and YAML linter from root guidance.
