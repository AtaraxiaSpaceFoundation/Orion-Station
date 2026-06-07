---
name: yaml-and-schema
description: Diagnose and author YAML, prototype schemas, workflow files, module manifests, and structured configuration safely.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Yaml And Schema

YAML syntax validity is only the first layer. Confirm the file's domain schema and runtime loader.

## Workflow

1. Identify the consumer: prototype manager, GitHub Actions, module loader, localization tooling, or custom config.
2. Copy structure from a current nearby file, not another fork.
3. Preserve scalar types, indentation, list/map shape, anchors, and quoting semantics.
4. Validate references and required fields.
5. Run both generic parsing and the domain-specific validator.

## Common failures

- valid YAML with an unknown prototype field;
- string parsed where a number or boolean is required;
- duplicate map keys;
- incorrect workflow expression quoting;
- frontmatter not starting on the first line;
- module manifest path that exists only in another repository;
- anchors or aliases rejected by a strict parser.

## Review

Keep diffs narrow and avoid reformatting unrelated blocks. Do not use a generic YAML formatter on serialized maps or files with repository-specific ordering expectations.
