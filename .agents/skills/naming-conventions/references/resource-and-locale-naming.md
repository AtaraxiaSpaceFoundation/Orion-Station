<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Resource and locale naming

Keep directory names and asset states stable after release. Match case exactly because Linux CI and clients may be case-sensitive.

FTL keys should identify feature and purpose, for example `bank-transfer-error-insufficient-funds`, not `error-2`. Sprite states should describe visual state, not the implementation that activates them. Audio collections should describe selection purpose and remain distinct from individual file names.
