---
name: resources-and-assets
description: Add and validate module resources, RSI sprites, textures, audio, maps, paths, attribution, and asset licenses.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Resources And Assets

Use the owning resource root. Orion-only assets belong in `Modules/Orion/Resources`.

## RSI

Keep `meta.json` valid, dimensions consistent, state names unique, frame data correct, and prototype references exact. Run the RSI validator.

## Paths

When moving an asset, update prototypes, code, stylesheets, XAML, maps, and collections. Search for the old path.

## Attribution

Record the original source and preserve required attribution and license metadata. Do not assume the repository's code license applies to third-party media.

## Review

Check case sensitivity, duplicate files, unused states, oversized assets, wrong module ownership, and accidental source-fork directory conventions.
