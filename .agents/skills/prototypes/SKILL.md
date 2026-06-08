---
name: prototypes
description: Create, inherit, compose, localize, and validate YAML prototypes in the correct resource root.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Prototypes

## Workflow

1. Verify repository identity, resource ownership, and edit-marker boundaries.
2. Find a current nearby prototype of the same type and owner.
3. Confirm the owning resource root from `module.yml`.
4. Verify fields against the destination component schema.
5. Prefer inheritance or composition over copying a large prototype.
6. Use stable specific IDs and typed IDs in code.
7. Add required English display text and mirror Russian structure and ordering.
8. Verify every referenced prototype, sprite, state, sound, dataset, map, and locale key.
9. Search for duplicate IDs and stale references.

Do not place repository-owned prototypes in another owner's root for convenience. Do not expose prototype IDs as fallback player text.

When English prototype localization changes, mirror Russian additions, removals, renames, attributes, variables, selectors, file paths, and ordered positions.

```powershell
git grep -n "PROTOTYPE_ID" -- Resources Modules
dotnet restore
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --check
```

Run the existing owner integration project when the prototype participates in server/client lifecycle or map loading.
