<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Coding and API design

Follow nearby repository style, but never substitute style matching for API verification.

## Symbol verification

Before calling, overriding, subscribing to, or constructing a non-local symbol, find its declaration and VERIFY:

- exact name and signature;
- declaring type and namespace;
- access modifier;
- static or instance context;
- project and assembly;
- caller project and assembly;
- project-reference path;
- lifecycle and thread requirements.

Do not code against a prompt example, old fork, search snippet, or similarly named member without confirming the current declaration.

## C# accessibility

| Modifier | Different type, same assembly | Different assembly |
|---|---|---|
| `private` | No | No |
| `internal` | Yes | No, unless explicit `InternalsVisibleTo` applies |
| `protected` | Only through a valid derived-type context | Only through a valid derived-type context |
| `protected internal` | Yes | Derived-type rules apply |
| `private protected` | Derived type in the same assembly only | No |
| `public` | Yes | Yes, with a valid project reference |

Additional invariants:

- a project reference does not override accessibility;
- a namespace does not define accessibility;
- an extension method has only the access of its declaring code;
- `partial` parts must compile into the same type and assembly;
- a module DLL cannot add a partial part to a core type;
- a `sealed` type cannot be subclassed;
- runtime-loaded assemblies do not grant compile-time type access.

If an implementation violates any invariant, it is invalid even if the code looks plausible.

## API design

Prefer a small explicit public hook over reflection, duplicated lifecycle code, or exposing mutable internals.

For entity actions:

- `On...` is a thin event entry point;
- `Can...` checks without mutation;
- `Try...` performs complete validation and returns success or failure;
- a dedicated execution step applies mutation.

`Try...` must not assume every caller already invoked `Can...`.

Keep components focused on serialized state. Put behavior in systems. Prefer existing system helpers, typed prototype IDs, and established entity APIs over raw manager access and magic strings.

Do not add an event merely to avoid calling an owning system. Use events for real decoupling or an established event flow.

## Forbidden workarounds

MUST NOT use reflection, `dynamic`, Harmony, unsafe field access, copied private logic, or duplicated startup/shutdown flows to pretend an inaccessible API is available, unless the user explicitly requests that technique and accepts its risks.

When no supported API exists, STOP and propose the smallest legitimate extension point.
