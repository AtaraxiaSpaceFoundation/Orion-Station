<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Orion Agent Guidance catalog

Read root `AGENTS.md` and the nearest scoped `AGENTS.md` first. Then select only the skills needed for the task.

## Planning and repository work

- `ai-workflow`: decompose broad work, delegate safely, preserve evidence, and verify delivery.
- `module-architecture`: choose module owner, assembly role, resource root, and dependency direction.
- `gameplay-feature`: route a complete multi-layer feature through the relevant skills.
- `code-review`: review correctness, regressions, authority, compatibility, resources, and tests.
- `git-workflow`: stage, commit, resolve conflicts, and deliver without losing user work.
- `documentation`: write technical guidance, PR notes, and verification reports.
- `upstream-maintenance`: keep inherited changes narrow and traceable.
- `porting`: port one complete source PR family into the modular destination.

## C# and ECS

- `csharp-style`: write repository-consistent C# with clear APIs and nullability.
- `ecs-basics`: read an unfamiliar ECS subsystem before changing it.
- `ecs-components`: design component state and serialization.
- `ecs-systems`: implement behavior, dependencies, subscriptions, and reusable APIs.
- `ecs-events`: choose local, directed, broadcast, cancellable, and network events.
- `entity-api-patterns`: use entity helpers, prototypes, spawning, deletion, popup, audio, and random APIs.
- `entity-lifecycle-and-spawning`: spawn, initialize, replace, transfer, and delete entities safely.
- `entity-relations-and-links`: maintain entity references, ownership links, and deletion cleanup.
- `serialization-and-datafields`: design prototype data, generated state, and compatibility-safe serialization.
- `naming-conventions`: name symbols, events, prototypes, FTL keys, and resources consistently.
- `timers-and-async`: manage delayed work, cancellation, entity lifetime, and main-thread application.

## Networking, authority, and input

- `client-server-shared`: place contracts, authority, prediction, and presentation correctly.
- `networking`: implement network events, component state, entity conversion, and dirtying.
- `prediction`: keep responsive actions deterministic and free of duplicate side effects.
- `pvs`: manage network interest and visibility without treating PVS as authorization.
- `interaction-flow`: structure event, `Try`, `Can`, and execution paths.
- `actions-and-doafter`: implement actions, cooldowns, interruption, and authoritative completion.
- `forms-and-input-validation`: parse and validate UI, command, numeric, text, and entity input.
- `security-and-validation`: review trust boundaries, permissions, abuse resistance, and secrets.
- `admin-and-permissions`: protect privileged commands, admin UI, and operator actions.
- `commands-and-cvars`: implement console commands and typed CVar-backed configuration.

## Content, localization, and resources

- `prototypes`: create, inherit, compose, and validate YAML prototypes.
- `prototype-localization`: connect prototype names, descriptions, markings, datasets, and selectors to FTL.
- `localization`: author FTL and apply repository localization policy.
- `localization-in-code`: use `LocId`, arguments, entity grammar, popups, and messages in C#.
- `yaml-and-schema`: author and diagnose YAML, workflows, manifests, and domain schemas.
- `resources-and-assets`: manage resource roots, RSI, textures, audio, maps, and attribution.
- `audio`: choose predicted, PVS, entity, coordinate, UI, ambient, or global audio flows.
- `appearance-and-visualizers`: replicate visual state and drive client presentation.
- `sprite-overlays-and-shaders`: implement layers, overlays, render targets, and shader lifecycles.
- `maps-and-mapping`: edit map files, map prototypes, grids, placements, and map compatibility.
- `collections-and-datasets`: maintain weighted collections, datasets, groups, and selection tables.
- `randomness-and-determinism`: use authoritative and testable randomness.

## UI and player-facing systems

- `xaml-ui`: build client XAML controls, windows, code-behind, and styles.
- `bound-user-interface`: implement entity-bound UI contracts, server validation, and state updates.
- `eui`: implement session-oriented UI lifecycle.
- `containers-and-inventory`: move entities through containers, hands, slots, storage, and equipment.
- `minds-roles-and-objectives`: work with sessions, minds, bodies, roles, antagonists, and objectives.
- `round-and-game-rules`: implement round lifecycle, rules, announcements, objectives, and cleanup.
- `damage-status-and-effects`: implement damage, healing, statuses, alerts, and modifiers.

## Gameplay domains

- `atmos`: modify gases, pressure, fire, pipes, devices, UI, and hot paths.
- `npc-ai`: modify HTN, navigation, steering, target selection, and AI debugging.
- `transform-and-physics`: work with maps, grids, coordinates, anchoring, fixtures, and collisions.
- `construction-and-machines`: implement construction graphs, machine lifecycle, power, parts, and upgrades.
- `chemistry-and-reagents`: modify reagents, solutions, reactions, transfer, metabolism, and chemistry UI.

## Persistence, external systems, and reliability

- `database-migrations`: maintain SQLite/PostgreSQL compatibility and upgrade paths.
- `save-data-and-configuration`: validate, migrate, and atomically write persistent files and configuration.
- `external-services`: integrate HTTP APIs, webhooks, retries, timeouts, caching, and secret-backed configuration.
- `logging-and-errors`: add useful diagnostics and player-safe error handling.

## Quality, debugging, and delivery

- `testing`: select build, test, YAML, RSI, and packaging checks.
- `tests-authoring`: design deterministic focused and integration regression tests.
- `debugging`: trace runtime behavior with logs, VV, breakpoints, and state comparison.
- `performance`: optimize measured hot paths without changing behavior.
- `build-and-packaging`: work with solution structure, CI, generated content, and release outputs.
