---
name: ecs-systems
description: Implement entity behavior, subscriptions, lifecycle handling, and reusable action APIs.
---

# Ecs Systems

Systems own behavior and state mutation.

## Structure

- Resolve dependencies with `[Dependency]`.
- Subscribe in `Initialize()`.
- Keep `On...` handlers thin.
- Route reusable actions through `Try...`.
- Put checks in `Can...` without mutation.
- Keep execution in one mutation path.
- Use existing system APIs before resolving lower-level managers.

## Entity access

Use `TryComp` for guard clauses, `Resolve` when a method accepts `Entity<T?>`, and `EnsureComp` for idempotent creation. Prefer `EntityQueryEnumerator` or established queries over repeated global searches.

Use deferred deletion in handlers or enumeration when immediate deletion could invalidate iteration.

## Lifecycle

Account for component startup, shutdown, map initialization, entity deletion, and predicted re-entry. Unsubscribe external callbacks and clean up overlays, timers, or cached references.

## Performance

Avoid allocations, LINQ, repeated prototype lookups, and broad queries in `Update()` or frequent events. Measure before redesigning a hot path.
