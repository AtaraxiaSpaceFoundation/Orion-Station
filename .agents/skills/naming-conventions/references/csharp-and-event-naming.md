# C# and event naming

Prefer names that reveal behavior without opening the implementation.

- `TryPurchase` implies validation plus execution and returns success.
- `CanPurchase` implies no mutation.
- `BeforePurchaseEvent` implies listeners may influence an upcoming operation.
- `PurchaseCompletedEvent` is a notification after mutation.
- `PurchaseAttemptEvent` should document cancellation or handled semantics.

Avoid vague names such as `Handle`, `Process`, `Data`, or `Manager` when a domain-specific name is available.
