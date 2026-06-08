---
name: yaml-and-schema
description: Author and diagnose YAML, prototype schemas, workflows, manifests, and structured configuration with exact validation.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Yaml And Schema

YAML syntax validity is only the first layer. Identify the actual consumer and schema.

## Workflow

1. Identify the loader: prototype manager, GitHub Actions, module loader, map serializer, localization tooling, or custom configuration.
2. Copy structure from a current nearby destination file.
3. Verify scalar types, indentation, list and map shape, quoting, anchors, and required fields.
4. Verify every ID, path, component field, and referenced resource.
5. Avoid formatting unrelated blocks or reserializing maps.
6. Run generic build validation and the domain-specific validator.

## Common failures

- valid YAML with an unknown component field;
- duplicate map keys;
- string where a number or boolean is required;
- invalid workflow expression quoting;
- frontmatter not starting on the first line;
- module manifest path from another repository;
- prototype field renamed in destination code;
- locale or prototype file placed outside the owner root.

## Exact validation commands

```powershell
dotnet restore
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --check
```

For workflow, map, RSI, or custom schema files, also run the exact specialized command from the current repository workflow. Do not guess from another fork.
