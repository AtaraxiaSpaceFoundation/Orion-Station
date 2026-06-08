---
name: prototype-localization
description: Keep prototype names, descriptions, markings, datasets, suffixes, selectors, and ordered Russian localization synchronized with English.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Prototype Localization

Prototype localization is part of the prototype contract.

1. Identify localization conventions for the prototype type.
2. Inspect the owning resource root and existing English and Russian files.
3. Search prototype IDs, current keys, alternative keys, and nearby family keys.
4. Use the existing owner file and mirrored relative path.
5. Treat English as the structural source of truth.
6. Add, remove, rename, move, and reorder Russian entries to match English.
7. Preserve variables, selectors, attributes, and literal semantic data.
8. Search stale references after ID or key changes.

New Russian prototype entries belong at the position corresponding to English, not automatically at the end.

Prototype IDs are machine identifiers and must not leak as display text. Russian text must be natural and must not contain `THE(...)` wrappers.

Run Release resource validation and inspect missing-key output.
