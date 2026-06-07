<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Entity replacement checklist

Decide explicitly whether to preserve or reset:

- map/grid coordinates and rotation;
- parent and container membership;
- mind, session, and ownership;
- hands, inventory, storage, and equipped items;
- damage, status effects, and actions;
- name, appearance, and metadata;
- BUI/EUI sessions;
- timers and external references;
- persistence identity.

A generic replacement helper may preserve a different subset than the feature requires. Confirm behavior in current code.
