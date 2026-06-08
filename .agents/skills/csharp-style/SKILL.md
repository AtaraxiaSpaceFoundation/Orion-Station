---
name: csharp-style
description: Write repository-consistent C# only after proving symbol access, ownership, and compatibility.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Csharp Style

Style never overrides correctness, accessibility, or assembly boundaries.

## Before writing a call

Find the declaration of every non-local symbol and verify its signature, modifier, namespace, project, assembly, caller context, and project reference.

Do not infer an API from autocomplete-like names, old forks, search snippets, prompt examples, or neighboring code.

## Accessibility invariants

- `private` is available only inside the declaring type.
- `internal` is available only inside the declaring assembly unless explicit friendship applies.
- `protected` requires a valid derived-type access context.
- a project reference does not bypass modifiers.
- an extension method cannot access private state.
- partial declarations cannot combine across assemblies.
- a module DLL cannot add methods or fields to a core partial type.
- a sealed type cannot be inherited.
- runtime module loading does not grant compile-time type access.

When required access is unavailable, stop and propose a real public or internal extension point in the correct owner. Do not use reflection, copied private logic, or visibility widening solely to make an implementation compile unless explicitly requested.

## Structure

Keep files focused and names discoverable. Prefer explicit domain names over generic `Manager`, `Data`, or `Helper`.

Keep public APIs small but sufficient for real callers. Do not expose mutable internals. If cross-assembly behavior is intended, design an explicit stable API rather than relying on implementation details.

## Nullability and entities

Respect nullable annotations and established entity/component patterns. Avoid null-forgiving operators unless an invariant is proven immediately nearby.

## Control flow

Prefer guard clauses. Keep event handlers thin. Avoid duplicated validation and deeply nested logic. Early returns must not bypass cleanup.

## Collections and allocation

Choose collections from semantics. Avoid unnecessary LINQ in hot paths and do not return mutable internal collections where callers should not mutate state.

## Comments and compatibility

Explain why, compatibility constraints, or non-obvious framework behavior. Do not narrate obvious code.

Treat public methods, events, serialized fields, prototype IDs, CVars, and network payloads as compatibility surfaces. Check all consumers before broad changes.
