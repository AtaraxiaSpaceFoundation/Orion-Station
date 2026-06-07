<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Localization workflow

1. Identify every player-visible string introduced or changed.
2. Find the owner's locale subtree.
3. Add or update the source locale key.
4. Update Russian when the wording is known and the feature supports it.
5. Pass variables from code or prototypes.
6. Search for the key to confirm spelling and usage.
7. Run resource validation.

Do not silently invent uncertain translations. Mark the limitation in the handoff when a translation requires domain review.
