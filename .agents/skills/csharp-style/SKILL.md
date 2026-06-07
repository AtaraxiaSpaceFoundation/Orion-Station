---
name: csharp-style
description: Write repository-consistent C# with clear APIs, nullability, ownership, and minimal incidental complexity.
---

# Csharp Style

Follow nearby code and project analyzers. Style supports correctness and searchability.

## Structure

Keep files focused and names discoverable. Use explicit domain names instead of generic `Manager`, `Data`, or `Helper` types. Prefer small public APIs and private implementation details.

## Nullability and entities

Respect nullable annotations. Use nullable entity/component patterns already established by the repository. Avoid null-forgiving operators unless an invariant is proven immediately nearby.

## Control flow

Prefer guard clauses for invalid state. Keep event handlers thin. Avoid deeply nested conditionals and duplicated validation. Use early returns without hiding cleanup responsibilities.

## Collections and allocation

Choose collection types from semantics. Avoid unnecessary LINQ in hot paths and avoid returning mutable internal collections. Use immutable or read-only views where callers should not mutate state.

## Comments

Explain why, compatibility constraints, or non-obvious framework behavior. Do not narrate obvious code. Keep TODOs actionable and linked to a real limitation.

## API compatibility

Treat public methods, serialized fields, events, prototype IDs, and network payloads as compatibility surfaces. Avoid broad signature changes without checking all consumers.
