---
name: save-data-and-configuration
description: Read, validate, migrate, and write persistent files and configuration without corruption or unsafe defaults.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Save Data And Configuration

Persistent files and configuration are compatibility and reliability surfaces.

## Reading

Treat files as untrusted input. Validate schema, bounds, required fields, enums, paths, and identifiers. Handle missing, empty, truncated, and malformed data with an explicit recovery policy.

## Defaults and normalization

Use safe defaults and normalize legacy values before exposing them to gameplay. Ensure defaults satisfy whitelists and cross-field constraints. Do not silently accept dangerous or impossible configuration.

## Writing

Write to a unique temporary file, flush where durability matters, then replace atomically. Clean temporary files in failure paths. Avoid overwriting a valid file with partial or invalid output.

## Migration

Version persistent formats when changes are not backward-compatible. Preserve unknown data only when the format and security model permit it. Back up unreadable legacy files before resetting them.

## Security

Constrain user-controlled paths, avoid shell interpolation, protect secrets, and set appropriate permissions. Do not log complete confidential configuration.

## Verification

Test missing file, valid legacy file, malformed file, partial write, concurrent write, invalid bounds, normalization, backup, and restart persistence.
