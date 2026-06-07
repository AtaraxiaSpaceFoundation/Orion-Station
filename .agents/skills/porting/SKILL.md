---
name: porting
description: Port one complete feature family into the modular repository with provenance, adaptation, dependencies, and validation.
---

# Porting

Use one destination PR per feature family.

A feature family may include the original source PR, later fixes, improvements, and follow-up PRs. Port the final intended behavior instead of cherry-picking every historical broken state.

## Workflow

1. Record source repository, root PR, included PRs, excluded or pending PRs, and source commit.
2. Build a complete file and dependency inventory: Common, Shared, Server, Client, prototypes, locale, UI, maps, sprites, audio, tests, and base modifications.
3. Determine target owner and module dependencies.
4. Use final source state as reference for feature-owned files.
5. Re-implement integration changes against current Goob Reforged APIs.
6. Preserve fixes from later PRs.
7. Verify code and asset licensing.
8. Test the complete feature in the destination.

Do not bundle independent systems merely because the source PR touched both. Port dependencies in separate PRs and stack branches when necessary.

Old `_Orion` paths are source paths only. Orion destinations live under `Modules/Orion`.
