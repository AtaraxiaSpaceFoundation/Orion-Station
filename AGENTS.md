<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Repository Agent Guidance

This is a modular Space Station 14 repository. Read this file first, then the nearest scoped `AGENTS.md`, relevant files in `.agents/rules/`, and only the skills selected through `.agents/CATALOG.md` or `.agents/SCENARIOS.md`.

## Requirement words

- **MUST** and **MUST NOT** are hard requirements.
- **VERIFY** means inspect the real declaration, file, project, directory, repository state, or command output before acting.
- **STOP** means do not invent a workaround. Report the blocking fact and the smallest valid next step.

## SPDX is human-owned metadata

Agents MUST NOT add, edit, delete, move, reorder, normalize, copy, or generate any line containing `SPDX-`.

This applies to existing and new files. If a new file requires SPDX metadata, leave the header for a human and list the path in the final report. Do not repair REUSE failures by changing SPDX.

The only exception is an explicit instruction in the user's current message that provides the exact SPDX change or exact header text.

## Verify repository identity before ownership decisions

Do not infer repository ownership from the local folder name, prompt wording, namespace, or an old fork.

Run before editing:

```powershell
git rev-parse --show-toplevel
git branch --show-current
git remote -v
git log -1 --oneline
git grep -n -E "[A-Za-z0-9]+-Edit(-Start|-End)?" -- "*.cs" "*.yml" "*.yaml" "*.ftl" "*.xml" "*.xaml" 2>$null
```

Record:

```text
Canonical repository:
Current repository owner tag:
Immediate upstream:
Owner module path:
Owner underscore paths:
Existing edit-marker syntax:
```

The owner tag is the short project identity established by the repository's own modules, namespaces, paths, and existing edit markers.

Owner-local paths include:

- `Modules/<OwnerTag>/...`
- directories named `_<OwnerTag>` or another verified owner-specific underscore path
- projects and namespaces explicitly owned by the current repository

An underscore directory such as `_<OwnerTag>` counts as owner-local for edit-marker decisions even when it is not a runtime module.

If repository identity, owner tag, upstream relationship, or owner paths are ambiguous, STOP before modifying inherited code.

## Edit markers in inherited files

Existing inherited files outside owner-local paths require the current repository's edit marker unless the change is explicitly intended for direct upstream submission.

Use the syntax already established in nearby files. If none exists, use:

- C# single-line change: `// <OwnerTag>-Edit`
- C# block: `// <OwnerTag>-Edit-Start` and `// <OwnerTag>-Edit-End`
- hash-comment formats: `# <OwnerTag>-Edit`
- XML, XAML, or Markdown: `<!-- <OwnerTag>-Edit -->`

MUST NOT add the repository marker:

- inside `Modules/<OwnerTag>/...`
- inside `_<OwnerTag>` or another verified owner-local underscore path
- inside another project already owned by the current repository
- to every line of a wholly new owner-local file
- to generated files, serialized maps, lockfiles, binary metadata, or formats where comments are invalid

Never use a foreign repository marker. Keep markers around the smallest inherited delta. Do not wrap an entire file when only one method or block changed.

## Evidence before code

Agents MUST NOT invent APIs, methods, events, types, paths, project references, prototype IDs, locale keys, resource paths, directory names, or framework behavior.

Before using a non-local symbol, VERIFY:

1. exact declaration and signature
2. declaring type and namespace
3. access modifier
4. owning project and assembly
5. caller project and assembly
6. required `ProjectReference`
7. runtime side, lifecycle, and thread constraints

Before creating a resource path, inspect the existing tree and nearest feature-owned files. A plausible path is not evidence.

## C# access boundaries are real

- `private` is accessible only inside the declaring type.
- `internal` is accessible only inside the declaring assembly unless applicable friendship exists.
- `protected` requires a valid derived-type access context.
- `public` still requires valid dependency direction and a project reference.
- Runtime loading does not grant compile-time access.
- Extension methods do not gain access to private or protected state.
- `partial` declarations do not combine across assemblies.
- A `sealed` class cannot be inherited from.

If required behavior is available only through inaccessible state and no supported extension point exists, STOP. Identify the declaration, modifier, involved assemblies, and smallest legitimate hook. Do not use reflection or runtime patching unless explicitly requested.

## Reuse existing ownership and infrastructure

Before creating a manager, service, system, event, helper, project, test project, fixture, CI step, MSBuild target, resource directory, or locale file, VERIFY that an equivalent owner or destination does not already exist.

Module integration tests belong in the existing `Content.<Module>.IntegrationTests` project. Test projects are not runtime modules and MUST NOT be added to `module.yml`.

## English localization is the structural source of truth

`en-US` is canonical for localization structure.

When English localization adds, removes, renames, moves, or reorders a message, attribute, section, variable, selector, or file, apply the same structural change to `ru-RU`.

The Russian file MUST mirror English message order. Insert a new Russian entry at the position corresponding to its English entry. Do not append it to the file end merely because that is easier.

Russian values must be natural translations. Message IDs, required attributes, variables, selectors, relative file paths, and message order must match English.

A Russian wording-only correction may leave English text unchanged, but it MUST preserve English-defined structure and order.

A Russian key absent from English is stale unless a verified framework requirement or documented repository convention proves it is intentionally locale-specific.

Before choosing an FTL path, inspect the actual locale trees and neighboring feature files. Reuse the existing owner file. Do not invent a cleaner hierarchy or shadow a core or foreign-module relative path.

Russian localization MUST NOT contain `THE(...)` or equivalent English grammar wrappers.

Do not persist or network already-resolved localized output. Long-lived UI must refresh localized text on culture changes without accumulating subscriptions.

## Mandatory preflight

```text
Repository and branch:
Canonical repository and upstream:
Repository owner tag:
Owner module and underscore paths:
Existing edit-marker syntax:
Requested outcome and exclusions:
Existing behavior owner:
Target and caller assemblies:
Required symbols and verified access:
Existing extension and test owners:
Locale files inspected:
Allowed and forbidden paths:
Verification commands:
```

Do not begin implementation while a correctness-critical field is unknown.

## Implementation expectations

- Inspect current implementation and file layout before designing a replacement.
- Prefer existing owning-system APIs and resource files over parallel helpers or directories.
- Keep components as data and mutations in systems.
- Validate all client-originated requests on the server.
- Update code, prototypes, English, Russian, resources, UI, and tests together when they form one feature.
- Treat serialized fields, prototype IDs, network payloads, database schema, maps, CVars, public APIs, locale keys, and locale variables as compatibility surfaces.
- Add tests at the owner that reproduce the real failure mode.
- Do not weaken assertions, add sleeps, suppress warnings, or use `continue-on-error` merely to obtain green CI.

## Canonical verification

Use the smallest set covering every changed surface:

```powershell
git status --short --branch
git submodule update --init --recursive
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet test --no-build --configuration Debug Content.Tests/Content.Tests.csproj -- NUnit.ConsoleOut=0 NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
$env:DOTNET_gcServer=1
dotnet test --no-build --configuration Debug Content.IntegrationTests/Content.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed NUnit.TestOutputXml="logs" NUnit.WorkDirectory="$(pwd)/test_results"
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --name-status
git diff --check
git diff -U0
```

Discover and run the existing integration-test project for each changed module. Run specialized RSI, map, database, and packaging commands from the current repository workflows. Report every applicable command not run and why.

## Delivery gate

Before reporting completion, VERIFY:

- every changed path belongs to scope
- repository identity and owner tag were verified
- edit markers were added only where ownership requires them
- owner module and owner underscore paths contain no redundant owner edit markers
- no foreign edit marker was introduced
- no agent-introduced SPDX line changed
- every called symbol exists and is accessible from the real caller
- every changed Russian file mirrors English keys, attributes, variables, selectors, relative path, and message order
- new Russian entries occupy the corresponding English position rather than the file end
- no duplicate project, fixture, CI step, manager, helper, locale file, or resource hierarchy was introduced
- all claimed commands actually ran
- the requested commit or remote update exists

Do not claim success based on intent.

## Git safety

Do not rewrite published history, discard user changes, remove untracked files, or run destructive cleanup without explicit approval for the exact operation.
