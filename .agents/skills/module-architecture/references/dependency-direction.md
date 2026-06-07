<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Dependency direction

Common must remain below Shared, Server, and Client.

Shared may depend on Common and base Shared APIs. It must not depend on Server or Client.

Server may depend on Common and Shared. Client may depend on Common and Shared. Server and Client must not depend on each other.

Base projects must not acquire references to a feature module. A module may extend base content through public APIs, events, prototypes, or narrow base changes.

Before adding a project reference, explain why an event, interface, shared contract, or owner-local implementation is insufficient.
