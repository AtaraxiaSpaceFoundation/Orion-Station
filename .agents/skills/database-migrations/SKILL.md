---
name: database-migrations
description: Change persistent models and SQLite/PostgreSQL migrations without breaking existing servers.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Database Migrations

Persistence is server-only compatibility work.

## Workflow

1. Find the owning database project and both provider conventions.
2. Update the model deliberately.
3. Consider existing rows, nullability, defaults, indexes, uniqueness, and backfills.
4. Generate matching SQLite and PostgreSQL migrations.
5. Review generated operations and snapshots.
6. Test upgrading an existing database, not only creating a new one.
7. Keep database I/O off frequent gameplay paths.

Do not use database entities as gameplay components or shared transport types. Avoid destructive schema changes unless data loss is explicitly accepted.

Report when migrations were generated but not exercised against both providers.
