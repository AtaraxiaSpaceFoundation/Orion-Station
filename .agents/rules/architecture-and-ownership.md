<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Architecture and Ownership

Determine the canonical repository, owner tag, real owner, assembly, and dependency direction before choosing a path.

## Repository ownership evidence

VERIFY:

1. origin and upstream remotes
2. current branch and target branch
3. existing repository edit markers
4. owner module and owner underscore directories
5. nearest implementation in root content and every module
6. owning `module.yml` and project files
7. caller and target assemblies
8. existing tests and extension points

Do not infer ownership from a filename or namespace alone.

## Owner-local paths

Treat these as owner-local:

- `Modules/<OwnerTag>/...`
- verified `_<OwnerTag>` directories
- projects explicitly created and maintained by the current repository

Owner-local files do not receive redundant owner edit markers.

Files inherited from an upstream repository and located outside owner-local paths receive the current repository marker around the smallest changed block, unless the change is explicitly upstream-ready.

## Dependency rules

Base content MUST NOT depend on a feature module. Module-to-module coupling MUST NOT be added for convenience. Runtime discovery is not a compile-time reference.

Moving a contract to Shared solely to bypass access is forbidden. Shared receives only state and contracts genuinely required by both sides.

## Access rules

A module is a separate assembly even when namespaces match.

- `partial` cannot join declarations across assemblies
- extension methods cannot read private state
- inheritance cannot bypass `private`
- `internal` requires the same assembly or explicit friendship
- runtime patching is not a normal extension point

If required behavior depends on inaccessible state, STOP and report the exact declaration, modifier, involved assemblies, and smallest reusable hook.

## Infrastructure ownership

Before creating a project, manager, fixture, loader, CI step, event bus, or MSBuild target, search for the existing owner.

A feature test belongs in the existing owner test project. Integration-test projects are not runtime modules and do not belong in `module.yml`.
