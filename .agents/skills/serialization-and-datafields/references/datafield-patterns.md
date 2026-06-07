<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# DataField patterns

Review every field for:

- who configures it;
- whether a default is safe;
- whether null is meaningful;
- whether a typed ID exists;
- whether the value is runtime-only or derived;
- whether maps or prototypes already serialize the old name;
- whether validation belongs at prototype load or runtime.

Collections need an explicit replacement or merge expectation. Mutable reference defaults must not be shared unintentionally between component instances.
