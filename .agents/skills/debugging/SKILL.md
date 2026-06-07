---
name: debugging
description: Investigate SS14 behavior using reproduction, event tracing, logs, VV, breakpoints, and targeted regression tests.
---

# Debugging

Debug before redesigning.

1. Reproduce the exact symptom.
2. Identify the authoritative owner.
3. Trace event subscription, validation, mutation, dirtying, and presentation.
4. Compare server and client state for networking issues.
5. Add temporary diagnostics only around the suspected divergence.
6. Confirm the failing assumption.
7. Fix the narrow cause.
8. Remove temporary diagnostics and add a stable regression test.

Useful questions:

- Did the event fire?
- Which guard returned?
- Did the component mutate?
- Was it dirtied?
- Did the client receive state?
- Did predicted code execute twice?
- Was the entity in PVS?
- Did a prototype or resource fail to load?

Do not use log spam as a substitute for tracing ownership.
