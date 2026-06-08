---
name: module-architecture
description: Prove repository ownership, module ownership, assembly boundaries, edit-marker rules, extension points, and test placement.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Module Architecture

## Mandatory workflow

1. verify origin, upstream, branch, and repository owner tag
2. identify owner module and verified underscore owner paths
3. search root content and every module for the existing behavior
4. read owner manifests, project files, scoped guidance, and solution entries
5. identify target and caller assemblies
6. locate every required declaration and access modifier
7. verify project-reference direction
8. find existing tests, fixtures, CI steps, and resource roots
9. choose Common, Shared, Server, Client, or Resources from actual dependencies

## Edit-marker boundary

Owner-local module and underscore paths do not receive redundant owner edit markers.

Inherited files outside those paths receive the current repository marker around the smallest changed block, unless the change is explicitly upstream-ready.

Do not use a foreign marker or add comments to invalid formats.

## Assembly decisions

Use Common for contracts required below gameplay Shared. Use Shared only for replicated contracts and prediction-safe state. Use Server for authority and hidden state. Use Client for presentation and UI.

Do not move code to Shared to bypass access. Do not introduce base-to-module references. Do not add module-to-module references merely to compile.

When a required member is inaccessible, STOP and identify the smallest public extension point. Do not copy private implementation or use reflection as an unrequested workaround.

## Existing infrastructure

Before creating anything, search for an existing module project, integration-test project, fixture, manifest entry, CI step, MSBuild target, manager, system, event, or service.

Build the affected project graph and run the existing owner test project.
