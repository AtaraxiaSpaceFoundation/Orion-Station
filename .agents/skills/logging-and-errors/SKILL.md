---
name: logging-and-errors
description: Add useful diagnostics, choose log levels, preserve context, and handle player-facing failures safely.
---

# Logging And Errors

Logs should help operators locate a failure without flooding normal operation or exposing sensitive information.

## Log levels

Use debug/verbose output for development-only detail, information for meaningful operational transitions, warnings for recoverable unexpected state, and errors for failed operations requiring investigation. Avoid logging normal rejection paths as errors.

## Context

Include subsystem, entity or prototype identity, operation, and important non-sensitive parameters. Prefer structured or consistent messages that can be searched. Do not log tokens, credentials, private messages, or complete user-provided payloads without necessity.

## Exceptions

Catch exceptions only when the caller can recover, add useful context, translate to a domain result, or perform cleanup. Do not swallow failures. Preserve stack information when rethrowing.

## Player-facing errors

Localize player messages and avoid exposing internal stack traces, file paths, SQL, or permission internals. Keep operator diagnostics separate from player feedback.

## Hot paths

Do not log every tick, every entity in a broad query, or every expected network rejection. Add rate limiting or aggregation when repeated diagnostics are necessary.

## Verification

Exercise success, expected rejection, malformed input, and exception paths. Confirm logs contain enough context and no secrets.
