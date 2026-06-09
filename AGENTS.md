<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Repository Agent Guidance

## Default scope

Work only within the smallest scope required by the current task.

Use paths, symbols, projects, modules, prototypes, locale keys, resources, and errors explicitly named by the user as the initial scope.

Do not inspect, inventory, summarize, or recursively enumerate the whole repository before starting work.

Do not read every `AGENTS.md`, `.agents/rules` file, skill, catalog, or scenario file.

Do not read `.agents/CATALOG.md` or `.agents/SCENARIOS.md` unless the user explicitly requests:

* a repository-wide review
* a full pull request review
* a complete feature port
* a broad architecture change
* a final pre-merge audit

Do not inspect unrelated modules, forks, upstream repositories, projects, resources, localization trees, tests, or workflows.

## Instruction selection

For a normal task, initially read only:

1. this root `AGENTS.md`
2. the nearest scoped `AGENTS.md` for each explicitly targeted path
3. no more than two directly relevant skills from `.agents/skills`

A third skill may be read only when the current code proves that another technical surface is directly affected.

Do not open a skill merely because it could be generally useful.

Do not read both a broad skill and all of its neighboring domain skills by default.

When expanding instruction scope, state the concrete changed surface that requires the additional instruction.

## Skill routing

Select skills from the actual requested change, not from hypothetical side effects.

* Local C# implementation: `.agents/skills/csharp-style/SKILL.md`
* Cross-project ownership or project references: `.agents/skills/module-architecture/SKILL.md`
* Client, server, or shared boundaries: `.agents/skills/client-server-shared/SKILL.md`
* Network state or network events: `.agents/skills/networking/SKILL.md`
* Predicted execution: `.agents/skills/prediction/SKILL.md`
* FTL localization: `.agents/skills/localization/SKILL.md`
* Localized values used from code: `.agents/skills/localization-in-code/SKILL.md`
* YAML prototypes: `.agents/skills/prototypes/SKILL.md`
* Prototype display text: `.agents/skills/prototype-localization/SKILL.md`
* General YAML or schema work: `.agents/skills/yaml-and-schema/SKILL.md`
* XAML controls: `.agents/skills/xaml-ui/SKILL.md`
* Bound user interfaces: `.agents/skills/bound-user-interface/SKILL.md`
* Assets or resource paths: `.agents/skills/resources-and-assets/SKILL.md`
* Maps: `.agents/skills/maps-and-mapping/SKILL.md`
* Creating or changing tests: `.agents/skills/tests-authoring/SKILL.md`
* Selecting verification commands: `.agents/skills/testing/SKILL.md`
* Build, solution, CI, or packaging changes: `.agents/skills/build-and-packaging/SKILL.md`
* Porting a complete feature: `.agents/skills/porting/SKILL.md`
* Maintaining inherited upstream code: `.agents/skills/upstream-maintenance/SKILL.md`
* Reviewing a pull request: `.agents/skills/code-review/SKILL.md`

Examples:

* A local C# fix normally requires only `csharp-style`.
* An FTL wording correction normally requires only `localization`.
* A prototype with a new visible name normally requires `prototypes` and `prototype-localization`.
* A networked component normally requires `client-server-shared` and `networking`.
* A XAML layout correction normally requires only `xaml-ui`.
* A test-only correction normally requires `tests-authoring` and `testing`.

## Targeted discovery

Search for exact symbols, paths, prototype IDs, locale keys, resource paths, errors, or directly related types.

Search commands must use the narrowest practical directory or pathspec.

Preferred examples:

```powershell
git grep -n "ExactSymbol" -- Modules/Orion/Content.Orion.Server
git grep -n "exact-locale-key" -- Modules/Orion/Resources/Locale
git grep -n "PrototypeId" -- Modules/Orion/Resources/Prototypes
Get-ChildItem Modules/Orion/Content.Orion.Server/Feature -File
dotnet build Modules/Orion/Content.Orion.Server/Content.Orion.Server.csproj --no-restore
```

Do not begin with unrestricted commands such as:

```powershell
Get-ChildItem -Recurse
git grep -n "generic-term"
rg "generic-term" .
dotnet build SpaceStation14.slnx
dotnet test
```

An unrestricted repository-wide search is allowed only when:

* the user explicitly requests repository-wide analysis
* the exact owner cannot be found through targeted searches
* a public compatibility surface is being renamed
* the task explicitly requires finding every reference

Stop expanding once enough evidence exists to implement the requested change.

## Repository identity

Do not perform a full repository identity audit for every task.

For work inside a clearly owner-local path such as `Modules/Orion`, treat that path as Orion-owned unless nearby project or module metadata contradicts it.

Run repository, remote, upstream, owner-tag, and edit-marker discovery only when the task changes:

* an inherited file outside owner-local paths
* module or project ownership
* project references
* upstream synchronization
* edit markers
* repository automation

When required, use targeted identity checks:

```powershell
git rev-parse --show-toplevel
git branch --show-current
git remote -v
git log -1 --oneline
```

Do not scan every source file for edit markers unless an inherited file is actually being changed.

## Evidence before changes

Do not invent APIs, symbols, events, types, paths, prototype IDs, locale keys, resources, projects, or framework behavior.

Verify declarations only for symbols that the implementation will actually call or modify.

For a non-local symbol, verify the declaration, accessibility, namespace, owning project, caller project, and required project reference.

Do not inspect unrelated assemblies or dependency graphs after the required access has already been proven.

If required state is inaccessible and no supported extension point exists, stop and report the concrete declaration and access boundary. Do not use reflection or copy private implementation logic.

## Ownership and edit markers

Never add, edit, remove, reorder, normalize, copy, or generate a line containing `SPDX-` unless the user's current request provides the exact SPDX change.

Owner-local paths do not require Orion edit markers.

Inherited files outside owner-local paths require the existing Orion edit-marker style around the smallest changed block.

Determine marker requirements only for files that will actually be modified.

Do not scan or classify unrelated files.

## Localization

For changed localization entries, `en-US` is the structural source of truth.

When an English message is added, removed, renamed, moved, reordered, or structurally changed, apply the matching change to the corresponding `ru-RU` file.

Russian localization must use natural wording and must not contain `THE(...)` or equivalent English grammar wrappers.

Compare only the affected locale files and directly referenced keys. Do not enumerate the complete locale tree for a local correction.

## Implementation

Inspect the current implementation and nearby files before introducing a new abstraction.

Prefer the existing owner, system, component, prototype file, resource directory, locale file, test project, and extension point.

Do not create parallel managers, helpers, projects, fixtures, CI steps, resource hierarchies, or locale files when an existing owner already covers the requested behavior.

Validate client-originated requests on the server when the task crosses a trust boundary.

Keep compatibility-sensitive identifiers stable unless the user explicitly requests their migration.

## Verification

Use the smallest verification set covering the files actually changed.

Do not automatically run:

* `git submodule update --init --recursive`
* `dotnet restore`
* a full solution build
* all unit tests
* all integration tests
* both Debug and Release builds
* every linter
* every packaging check

For a local C# change, build the directly affected project.

For a test change, run the directly affected test project or filtered test.

For localization or prototype changes, run only the relevant validation when such a targeted command exists.

For documentation or agent-instruction changes, use diff checks only unless executable behavior changed.

Always run:

```powershell
git diff --check
git diff --stat
```

Inspect the final diff for the files changed by the task.

Run full repository verification only when the user explicitly requests full validation, final PR validation, release validation, or a repository-wide audit.

Do not include large successful build or test logs in model context. Preserve the command result and inspect only relevant warnings or failures.

When a command fails, narrow the output to the first actionable errors before further analysis.

## Delivery

Before reporting completion, verify only the surfaces touched by the task:

* changed files belong to the requested scope
* used symbols exist and are accessible
* required localization counterparts were updated
* no unrelated files were modified
* claimed verification commands actually ran
* the final diff matches the requested outcome

Do not claim checks that were not run.

Do not rewrite published history, discard user changes, remove untracked files, or perform destructive cleanup without explicit approval.
