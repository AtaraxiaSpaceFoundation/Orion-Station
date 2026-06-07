<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Module guidance

Each direct child of `Modules/` with a `module.yml` is an independent owner. Read the manifest before editing code or resources.

Keep module code in its declared Common, Shared, Server, and Client projects. Keep module resources under the manifest's resource roots. Verify project references before introducing a dependency.

Do not use one module as a dumping ground for another module's feature. If base content must change to expose an extension point, keep that change minimal and leave the feature implementation in its owning module.
