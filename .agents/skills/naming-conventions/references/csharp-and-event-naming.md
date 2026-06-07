<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# C# and event naming

Prefer names that reveal behavior without opening the implementation.

- `TryPurchase` implies validation plus execution and returns success.
- `CanPurchase` implies no mutation.
- `BeforePurchaseEvent` implies listeners may influence an upcoming operation.
- `PurchaseCompletedEvent` is a notification after mutation.
- `PurchaseAttemptEvent` should document cancellation or handled semantics.

Avoid vague names such as `Handle`, `Process`, `Data`, or `Manager` when a domain-specific name is available.
