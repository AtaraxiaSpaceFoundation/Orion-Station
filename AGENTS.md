<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion Agent Guidance

This repository is a modular Space Station 14 codebase based on Goob Reforged. Read this file first, then load the nearest scoped `AGENTS.md`, the relevant files in `.agents/rules/`, and only the skills needed for the current task.

Use `.agents/CATALOG.md` to select skills. Use `.agents/SCENARIOS.md` when a task spans several technical areas and the correct routing is unclear.

## Working order

1. Inspect the current implementation before designing a replacement.
2. Search root Content projects and every module for an existing owner.
3. Read the selected module's `module.yml`, project references, and nearby code.
4. Keep the change in the narrowest correct owner and assembly.
5. Preserve server authority, prediction behavior, localization, resources, compatibility, and tests.
6. Run the smallest meaningful verification and report what was not run.
7. Inspect the final diff, staged paths, and delivery state before reporting completion.

## Repository ownership

- Root `Content.Common`, `Content.Shared`, `Content.Server`, `Content.Client`, and root `Resources` are base content.
- Each direct child of `Modules/` with a `module.yml` is an independent content owner.
- Orion-only work belongs under `Modules/Orion` unless a module-local extension is impossible.
- GoobStation and Lavaland are separate owners. Do not place Orion-specific behavior there for convenience.
- Root-content focused and integration tests belong in `Content.Tests` and `Content.IntegrationTests`; module-specific integration tests belong in `Modules/<Module>/Content.<Module>.IntegrationTests`.
- Do not place Orion-only integration tests in root `Content.IntegrationTests` without an architectural reason. A modular integration-test project is not a runtime project in `module.yml` and must be connected to both `SpaceStation14.slnx` and CI.
- `RobustToolbox/` is the engine boundary. Edit it only when content-side APIs cannot solve the task.

## Assembly roles

- **Common**: types that do not depend on gameplay Shared, Server, or Client assemblies.
- **Shared**: replicated contracts, components, events, and prediction-compatible behavior.
- **Server**: authority, validation, hidden state, persistence, and protected decisions.
- **Client**: rendering, presentation, input presentation, visuals, and user interfaces.
- **Resources**: prototypes, localization, textures, RSI files, audio, maps, and other content data.

Never move hidden or trusted state to Shared merely to simplify access. Never let the client decide protected outcomes.

## Core expectations

- Prefer existing `EntitySystem` APIs and nearby patterns over parallel helper layers.
- Keep components as data and mutations in systems.
- Keep event handlers thin and route reusable actions through `Try...`, checks through `Can...`, and execution through a dedicated mutation step.
- Validate all client-originated requests on the server.
- Localize all player-visible text. Do not compare localized text in game logic.
- Update code, prototypes, resources, locale, UI, and tests together when they form one feature.
- Treat serialized fields, prototype IDs, network payloads, database schema, maps, and CVars as compatibility surfaces.
- Avoid unrelated cleanup in upstream or inherited files.
- Do not claim tests passed unless they were actually run.

## Porting policy

Use one destination PR per feature family. A feature family may include the original source PR and later fixes or improvements. Port the final intended behavior, not every historical broken state. Record source repository, source PRs, source commit, dependencies, target module, assets, licenses, tests, and omitted work.

The old `Orion-Station-14` repository is a possible source during migration. Its root `_Orion` paths are source evidence only, never target paths in this repository.

## Guidance hierarchy

1. Root `AGENTS.md` defines repository-wide requirements.
2. Scoped `AGENTS.md` files refine ownership and directory behavior.
3. `.agents/rules/` contains always-on constraints.
4. `.agents/CATALOG.md` routes tasks to skills.
5. `.agents/skills/*/SKILL.md` contains task workflows.
6. Skill `references/` contain detailed checklists and patterns.
7. Tool-specific files are adapters and must not become separate sources of truth.

## Git safety

Do not force-push, rewrite history, discard user changes, remove untracked files, or run destructive cleanup without explicit approval for the exact command. Inspect `git status` before staging and keep unrelated files out of the commit.
