---
name: porting
description: Port one complete feature family with provenance, current-API adaptation, repository ownership, localization, resources, and verification.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Porting

Use one destination change per feature family. Port final intended behavior, including later fixes, rather than historical broken states.

Record the source repository, source commit, root PR, follow-ups, exclusions, dependencies, and licensing evidence. Inventory code layers, prototypes, English, Russian, UI, maps, sprites, audio, tests, database changes, CVars, and every inherited-file modification.

Verify the destination repository identity, owner tag, owner module, owner underscore paths, current APIs, project references, resources, and test infrastructure.

Do not assume a source API, path, field, prototype parent, test project, marker, or locale layout exists in the destination.

English localization is structurally canonical. Add natural Russian localization and mirror English keys, attributes, variables, selectors, file paths, and order.

Repository-owned behavior belongs in owner-local paths when possible. Inherited edits require the destination repository marker around the smallest delta. Owner-local module and underscore paths do not receive redundant markers.

Do not bundle independent systems or create duplicate build and test infrastructure.

Run the Debug build, Release resource validation, root integration tests, and existing integration-test projects for changed modules. Report omitted source behavior and unavailable checks.
