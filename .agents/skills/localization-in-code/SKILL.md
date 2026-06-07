---
name: localization-in-code
description: Use LocId, Loc.GetString, variables, entity grammar, popups, and messages safely in C#.
---

# Localization In Code

Use localization APIs at the presentation boundary. Keep internal logic typed and language-independent.

## LocId and fields

Use `LocId` for configurable locale references when the surrounding serialization supports it. Keep required and optional keys explicit. Do not store already-localized output as persistent state.

## Arguments

Pass typed values, entities, counts, durations, and prototype-derived text as variables. Keep variable names stable and meaningful. Verify that FTL selectors match the supplied type.

## Entity text

Use current entity-name and grammar helpers rather than manually concatenating names, pronouns, or articles. Avoid double-localizing names that are already resolved by an entity helper.

## Popups and chat

Choose the correct audience and prediction-aware helper. Do not expose server-only error details to players. Reuse one localized failure reason across UI, popup, and chat only when the semantics truly match.

## Review failures

- raw enum or prototype ID shown to players;
- hardcoded strings in system code;
- missing variables;
- inconsistent variable names between code and FTL;
- localized string comparisons;
- duplicate predicted popups;
- server logs routed through player locale.
