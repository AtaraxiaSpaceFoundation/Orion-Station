---
name: entity-api-patterns
description: Use common EntitySystem helpers, prototype APIs, spawning, deletion, audio, popup, and random patterns.
---

# Entity Api Patterns

Prefer the highest-level existing helper that expresses the operation.

Common entity helpers include `HasComp`, `TryComp`, `CompOrNull`, `Resolve`, `EnsureComp`, `RemCompDeferred`, `Transform`, `MetaData`, `Name`, and `EntityQueryEnumerator`.

Use typed prototype IDs. Use `Index` when absence is a data error and `TryIndex` when absence is an expected branch.

Choose spawning helpers from attachment and placement semantics instead of converting coordinates unnecessarily. Use deferred deletion when inside event dispatch or enumeration.

For predicted shared actions, use the repository's predicted popup and audio APIs. Do not use non-deterministic randomness in predicted logic unless the surrounding API provides a deterministic approach.

Search nearby code because helper signatures and preferred overloads may change with RobustToolbox updates.
