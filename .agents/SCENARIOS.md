<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Scenario Routing

Root hard rules always apply.

## Any write task

Before editing:

1. verify repository, branch, remotes, upstream, and owner tag
2. identify owner module and owner underscore paths
3. inspect existing edit-marker syntax
4. locate declarations, assemblies, resources, tests, and locale owners
5. preserve the existing SPDX diff

## Change an inherited file

Read `module-architecture`, `upstream-maintenance`, and `git-workflow`.

- Add the current repository marker around the smallest changed inherited block.
- Do not use a foreign marker.
- Do not add the marker inside owner-local module or underscore paths.
- Do not add comments to formats that cannot safely contain them.

## Change an owner-local file

Owner-local includes `Modules/<OwnerTag>`, verified `_<OwnerTag>` paths, and other projects proven to belong to the current repository.

Do not add redundant owner edit markers there. Follow the scoped guidance and nearby style.

## Add or update localization

Read `localization`, `localization-in-code`, and the owning domain skill.

- Treat `en-US` as structural truth.
- Mirror additions, deletions, renames, moves, attributes, variables, selectors, and ordering in `ru-RU`.
- Insert a new Russian message at the corresponding English position, not at the file end.
- Reuse existing mirrored files and preserve exact path spelling.
- Write natural Russian without `THE(...)` wrappers.

```powershell
Get-ChildItem Resources/Locale -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
Get-ChildItem Modules -Recurse -File -Filter *.ftl | Select-Object -ExpandProperty FullName
git grep -n -E "EXACT_KEY|OLD_KEY|PROPOSED_KEY|FEATURE_PREFIX" -- Resources Modules Content.*
git diff -- "*.ftl"
```

## English localization changed

Compare the affected English and Russian files as ordered message sequences.

- English key added: add Russian key in the same relative position.
- English key removed: remove the Russian counterpart.
- English key renamed: rename the Russian counterpart and search stale references.
- English block reordered: reorder Russian messages the same way.
- English file moved or renamed: mirror the Russian file path.
- English variables, selectors, or attributes changed: mirror the contract exactly.

## Russian-only wording correction

Change only the translated value when structure is unchanged. Do not reorder keys, rename variables, add locale-only keys, or rewrite English without a semantic reason.

## Add or update a module integration test

Read the nearest scoped guidance, `testing`, and `tests-authoring`.

Use the existing `Content.<Module>.IntegrationTests` project. Do not create duplicate fixtures, CI steps, MSBuild targets, or `module.yml` entries.

## Required private or internal state is inaccessible

VERIFY declarations and assemblies. Do not use cross-assembly partial classes, extension methods, reflection, or copied private logic. STOP and identify the smallest reusable extension point.

## Review a broad change

Read `code-review` and all domain skills matching changed files. Verify ownership, marker placement, English/Russian order parity, resources, authority, lifecycle, compatibility, tests, and command evidence.
