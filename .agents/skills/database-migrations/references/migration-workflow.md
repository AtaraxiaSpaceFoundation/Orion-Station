# Migration workflow

For each schema change document:

- model field or relationship;
- SQLite migration;
- PostgreSQL migration;
- default/backfill for existing data;
- indexes and constraints;
- rollback or failure expectations;
- upgrade verification.

Search for provider-specific differences before assuming generated SQL is equivalent.
