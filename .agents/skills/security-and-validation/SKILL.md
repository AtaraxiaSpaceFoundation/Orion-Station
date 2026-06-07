---
name: security-and-validation
description: Review trust boundaries, input validation, permissions, abuse resistance, secrets, and safe failure behavior.
---

# Security And Validation

Treat all client, command, file, network, and external-service input as untrusted until validated by the authoritative owner.

## Validate

Check identity, ownership, permissions, target validity, range, state, quantity, bounds, rate limits, resource availability, and replay behavior. Recompute protected values on the server instead of trusting client displays.

## Fail safely

Reject malformed or unauthorized requests without partial mutation. Avoid exception-driven control flow for expected rejection. Rate-limit repeated abuse and avoid revealing hidden state through distinct error messages.

## Secrets

Never commit tokens, credentials, private keys, webhook secrets, or production connection strings. Use repository secrets and environment configuration. Avoid logging sensitive payloads.

## Files and paths

Normalize and constrain user-controlled paths. Prevent traversal, arbitrary overwrite, shell interpolation, and unsafe temporary-file behavior. Use atomic replacement and explicit permissions where persistence requires it.

## Verification

Test unauthorized access, malformed payloads, boundary values, replay, concurrent requests, stale targets, and partial failure.
