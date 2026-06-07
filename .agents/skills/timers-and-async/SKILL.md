---
name: timers-and-async
description: Manage timers, cancellation, entity lifetime, asynchronous work, and main-thread state safely.
---

# Timers And Async

Timers and asynchronous work must respect entity lifetime and simulation ownership.

## Timers

Use the established game timing and timer APIs. Define who owns cancellation. Before a callback mutates state, re-check that entities and components still exist and that the operation is still relevant.

Do not capture stale component references across delayed callbacks. Avoid creating one timer per entity in hot systems when an update loop or shared scheduler is the established pattern.

## Async work

Keep ECS mutation on the correct simulation thread. Background work may perform isolated I/O or computation only when the API explicitly supports it. Marshal results back through the established mechanism and revalidate state before applying them.

## Round and map lifecycle

Cancel or invalidate work on round restart, map unload, entity deletion, user disconnect, and component removal when applicable.

## Common failures

- callback runs after entity deletion;
- duplicate timer starts apply an effect twice;
- cancellation token is ignored;
- background thread touches entity state;
- timer captures an outdated price, permission, or target;
- repeating timer survives system shutdown.

## Verification

Test cancellation, deletion, repeated scheduling, shutdown, disconnect, exception paths, and delayed completion after state changes.
