<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Architecture and ownership

Determine the owner before deciding the path.

1. Search root Content projects and every `Modules/*` tree for the nearest existing implementation.
2. Read the candidate owner's `module.yml`.
3. Read the relevant `.csproj` references and `SpaceStation14.slnx`.
4. Choose Common, Shared, Server, Client, or Resources from dependency needs, not from filename similarity.
5. Prefer an owner-local extension over changing inherited base or another module.

A module may depend on base content through its project references. Do not introduce a reverse dependency from base content into a module. Do not create module-to-module coupling unless the manifests and project references deliberately support it.

Root `_Orion` directories from the old repository are migration source paths only. New Orion code belongs under `Modules/Orion`.

When a feature crosses assemblies, keep the contract in the lowest safe layer and side-specific behavior in Server or Client. Avoid convenience references that expose hidden server state or client-only UI types to Shared.
