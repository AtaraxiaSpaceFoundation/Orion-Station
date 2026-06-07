---
name: actions-and-doafter
description: Implement actions, cooldowns, DoAfter flows, interruption, cancellation, and authoritative completion safely.
---

# Actions And DoAfter

Use actions for reusable player abilities and DoAfter flows for interruptible, time-based completion.

## Action lifecycle

1. Find the owner component and system.
2. Decide whether the action is granted by prototype, component startup, equipment, role, or runtime state.
3. Keep action identity and event contract in Shared when client prediction or UI needs it.
4. Validate user, target, cooldown, range, access, and current state on the server.
5. Start the action or DoAfter exactly once.
6. Apply resources, cooldowns, and persistent effects only at the correct authoritative point.
7. Remove or disable the action when its source disappears.

## DoAfter design

Define interruption rules deliberately: movement, damage, hand changes, target movement, target deletion, range, incapacitation, and duplicate requests. Use the established DoAfter event pattern in the current codebase.

Do not mutate valuable state before completion unless rollback is explicit. Completion handlers must revalidate state because the world may have changed during the delay.

## Prediction

Immediate local animation, popup, or sound may be predicted. The final protected outcome remains authoritative. Prevent duplicate feedback when the server confirms or rejects the action.

## Common failures

- action remains after the granting component is removed;
- cooldown is applied before a rejected attempt;
- two concurrent DoAfters complete the same operation;
- target deletion leaves stale state;
- completion trusts conditions checked only at start;
- predicted sound or popup plays twice.

## Verification

Test success, cancellation by every configured interrupt, repeated requests, target deletion, disconnect, source removal, cooldown, and rejection under latency.
