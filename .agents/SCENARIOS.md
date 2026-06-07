# Scenario routing

This file shows which guidance to load for common Orion tasks. It is a routing aid, not a substitute for reading current code.

## Add a networked Orion component

Read:

- `Modules/Orion/Content.Orion.Shared/AGENTS.md`
- architecture and gameplay-networking rules
- `module-architecture`
- `ecs-components`
- `serialization-and-datafields`
- `networking`
- `testing`

Confirm the client needs every replicated field and dirty authoritative changes.

## Add a predicted interaction

Read:

- nearest Shared and Server guidance
- interactions-and-authority rule
- `interaction-flow`
- `prediction`
- `networking`
- `audio` or `localization-in-code` for feedback
- `tests-authoring`

Trace repeated execution, first-time side effects, server rejection, and reconciliation.

## Add BUI and XAML

Read:

- Shared, Server, Client, and Resources scoped guidance
- `bound-user-interface`
- `xaml-ui`
- `forms-and-input-validation`
- `localization`
- `testing`

Keep messages as intent, revalidate on the server, and refresh state after mutation.

## Add prototype, RSI, audio, and FTL

Read:

- owning Resources guidance
- `prototypes`
- `prototype-localization`
- `resources-and-assets`
- `audio` or `appearance-and-visualizers`
- `yaml-and-schema`
- `testing`

Verify all IDs, paths, sprite states, locale keys, attribution, and asset licenses.

## Port one old Orion feature family

Read:

- `Modules/Orion/AGENTS.md`
- `porting`
- `module-architecture`
- `upstream-maintenance`
- `third-party-materials` rule
- relevant gameplay and UI skills
- `testing`

Build a source PR family manifest, use final source behavior, and adapt integration to current Goob Reforged code.

## Fix a client/server desync

Read:

- `client-server-shared`
- `networking`
- `prediction`
- `pvs`
- `debugging`
- `tests-authoring`

Compare authoritative mutation, dirtying, PVS delivery, client application, and repeated prediction.

## Add database persistence

Read:

- Server scoped guidance
- `database-migrations`
- `serialization-and-datafields`
- `security-and-validation`
- `tests-authoring`

Review both SQLite and PostgreSQL upgrade paths and existing data defaults.

## Add a game rule or objective

Read:

- Server and Shared guidance
- `round-and-game-rules`
- `minds-roles-and-objectives`
- `actions-and-doafter` if player actions are granted
- `localization`
- `tests-authoring`

Test late join, reconnect, body transfer, no eligible players, round end, and restart cleanup.

## Optimize a hot event

Read:

- `performance`
- `ecs-systems`
- `ecs-events`
- owning domain skill
- `testing`

Measure or establish event frequency, preserve behavior, and avoid allocations, global scans, and unnecessary dirtying.

## Add an external API integration

Read:

- `external-services`
- `commands-and-cvars`
- `security-and-validation`
- `save-data-and-configuration` when caching or persisting
- `timers-and-async`
- `logging-and-errors`
- `tests-authoring`

Define timeout, cancellation, retry, stale-result, privacy, and service-outage behavior.

## Review a broad PR

Read:

- `code-review`
- `module-architecture`
- domain skills matching changed files
- `testing`
- `security-and-validation` for protected inputs

Report concrete findings by severity with paths, execution sequence, impact, and minimal remediation.
