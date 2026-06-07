---
name: forms-and-input-validation
description: Validate UI, command, text, numeric, entity, and configuration input consistently across client feedback and server authority.
---

# Forms And Input Validation

Validation has two layers: client feedback and authoritative server enforcement.

## Parsing

Trim input deliberately. Parse numbers with an explicit culture and format policy. Reject ambiguous values such as integer fields supplied as `1.0` unless the domain permits them. Bound lengths before expensive processing.

## Domain validation

Validate ranges, whitelists, prototype IDs, entity existence, ownership, access, cooldowns, and cross-field constraints. Normalize case and whitespace only when the domain defines them as insignificant.

## Error reporting

Return specific localized errors without exposing hidden state or internal exceptions. Keep one validation source of truth so UI and server do not drift.

## Security

Treat rich text, markup, file paths, URLs, and command fragments as hostile. Apply escaping or structured APIs rather than concatenating into shell, SQL, or markup.

## Common failures

- client-only validation;
- culture-dependent decimal parsing;
- overflow or negative quantity;
- stale entity selected in UI;
- whitelist comparison with inconsistent casing;
- normalized value differs from stored value;
- error message reveals protected data.

## Verification

Test empty, whitespace, minimum, maximum, overflow, locale variants, malformed markup, stale entities, unauthorized actors, and conflicting fields.
