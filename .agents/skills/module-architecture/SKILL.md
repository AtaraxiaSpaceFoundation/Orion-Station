---
name: module-architecture
description: Choose the correct base project or module, assembly role, resource root, and dependency direction.
---

# Module Architecture

Use this before placing a new feature or moving code.

## Workflow

1. Search root Content and every module for related components, systems, prototypes, UI, tests, and resources.
2. Identify the existing owner from behavior, not just path names.
3. Read the owner's `module.yml`, `.csproj` references, and `SpaceStation14.slnx`.
4. Select Common, Shared, Server, Client, or Resources from actual dependencies.
5. Prefer an owner-local extension.
6. Re-check that the new reference direction is valid.

## Decisions

Use Common only for types required below gameplay Shared. Use Shared for contracts and prediction-compatible behavior. Use Server for authority and persistence. Use Client for visuals and UI. Use the resource root declared by the owner manifest.

Do not introduce base-to-module references. Do not add module-to-module coupling for convenience. Do not use old root `_Orion` paths as destinations.

## Verification

Build the affected project graph and confirm documented paths exist.
