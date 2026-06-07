# Orion Agent Guidance

This repository is a modular Space Station 14 codebase based on Goob Reforged. Read this file first, then load the nearest scoped `AGENTS.md`, the relevant files in `.agents/rules/`, and only the skills needed for the current task.

## Working order

1. Inspect the current implementation before designing a replacement.
2. Search root Content projects and every module for an existing owner.
3. Read the selected module's `module.yml`, project references, and nearby code.
4. Keep the change in the narrowest correct owner and assembly.
5. Preserve server authority, prediction behavior, localization, resources, and tests.
6. Run the smallest meaningful verification and report what was not run.

## Repository ownership

- Root `Content.Common`, `Content.Shared`, `Content.Server`, `Content.Client`, and root `Resources` are base content.
- Each direct child of `Modules/` with a `module.yml` is an independent content owner.
- Orion-only work belongs under `Modules/Orion` unless a module-local extension is impossible.
- GoobStation and Lavaland are separate owners. Do not place Orion-specific behavior there for convenience.
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
- Update code, prototypes, resources, and locale together when they form one feature.
- Avoid unrelated cleanup in upstream or inherited files.
- Do not claim tests passed unless they were actually run.

## Porting policy

Use one destination PR per feature family. A feature family may include the original source PR and later fixes or improvements. Port the final intended behavior, not every historical broken state. Record source repository, source PRs, source commit, dependencies, target module, and omitted work.

The old `Orion-Station-14` repository is a possible source during migration. Its root `_Orion` paths are source evidence only, never target paths in this repository.

## Guidance map

Always-on rules live in `.agents/rules/`.

Task-specific instructions live in `.agents/skills/`. Important categories include:

- module placement and dependency direction;
- ECS components, systems, events, and entity APIs;
- client/server/shared networking, dirtying, prediction, and PVS;
- interactions, prototypes, localization, resources, audio, and visuals;
- XAML, BUI, and EUI;
- testing, debugging, performance, persistence, atmos, NPC AI, and physics;
- porting, upstream maintenance, documentation, and complete gameplay features.

Scoped `AGENTS.md` files override or refine this document for their directory tree.

## Git safety

Do not force-push, rewrite history, discard user changes, remove untracked files, or run destructive cleanup without explicit approval for the exact command. Inspect `git status` before staging and keep unrelated files out of the commit.
