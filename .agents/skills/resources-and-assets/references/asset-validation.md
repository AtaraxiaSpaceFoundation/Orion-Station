<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Asset validation

For every asset change verify:

- owning module and resource root;
- source URL or source repository revision;
- license and attribution;
- exact case-sensitive path;
- prototype and code references;
- RSI dimensions, states, and frame metadata;
- audio format and collection references;
- removal of replaced paths.

Use `RobustToolbox/Schemas/validate_rsis.py` for RSI work after the submodule and Python dependencies are available.
