# Coding and API design

Follow nearby repository style before introducing a new abstraction.

For entity actions, prefer:

- `On...` for the thin event entry point;
- `Try...` for the reusable public action;
- `Can...` for checks without mutation;
- a dedicated execution step for state changes.

`Try...` should perform its own validation instead of assuming every caller already called `Can...`. Use `bool` for normal success/failure unless the surrounding API uses a richer result.

Prefer `[Dependency]` fields over resolving services inside method bodies. Use `Resolve(...)` when a public method accepts `Entity<T?>`. Use `TryComp`, `CompOrNull`, `EnsureComp`, typed prototype IDs, and existing system helpers instead of manual manager access or raw strings.

Keep components focused on serialized state. Put behavior in systems. Avoid magic prototype IDs, raw resource paths, repeated string keys, and one-off special cases when data or an existing API can express the behavior.

Do not add an event merely to avoid calling an owning system. Events are appropriate when multiple independent listeners need decoupling or when the existing interaction flow already uses an event.
