<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# DoAfter checklist

- start request is authoritative or validated;
- user and target entities are resolved;
- duplicate starts are prevented where necessary;
- interruption flags match gameplay intent;
- state is revalidated on completion;
- costs are charged once;
- cancellation leaves no partial state;
- UI and feedback reflect cancellation;
- target deletion and user deletion are safe;
- integration coverage includes latency-sensitive paths.
