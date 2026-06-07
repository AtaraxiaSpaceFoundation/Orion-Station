---
name: interaction-flow
description: Implement reusable interactions, verbs, in-hand use, actions, and BUI requests with clear validation and mutation.
---

# Interaction Flow

Use a consistent flow:

1. `On...` receives the event and handles routing flags.
2. `Try...` is the reusable public API.
3. `Can...` checks state without mutation.
4. Execution performs all mutations and side effects.

`Try...` must call its own checks. A UI or verb may perform an early client-side check for feedback, but the authoritative server path must validate again.

Keep `Can...` quiet-capable when callers need non-spamming probes. Do not decrement resources, change components, or spawn entities in `Can...`.

Set handled or cancelled flags only when the system truly consumed the action. Add tests for rejection and repeated invocation when the interaction changes valuable or persistent state.
