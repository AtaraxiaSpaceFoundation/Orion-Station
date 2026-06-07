---
name: external-services
description: Integrate HTTP APIs, webhooks, external processes, retries, timeouts, caching, and secret-backed configuration safely.
---

# External Services

External systems are unreliable and untrusted. Keep gameplay resilient when they are slow or unavailable.

## Configuration and secrets

Use CVars, environment variables, or repository secret mechanisms. Never commit credentials or production endpoints that contain secrets. Validate URLs and configuration at startup.

## Requests

Set explicit timeouts, cancellation, bounded response sizes, and supported content types. Validate status codes and response schemas before use. Do not deserialize arbitrary polymorphic payloads.

## Reliability

Define retry policy only for idempotent operations. Use bounded backoff and avoid retry storms. Cache stable data where appropriate and define stale-data behavior.

## Simulation boundary

Do not block the main simulation thread on network I/O. Apply results through the supported main-thread path and revalidate entity or round state before mutation.

## Privacy and logging

Send only required data. Avoid player-identifying or private content unless the feature explicitly requires and documents it. Redact secrets and payloads from logs.

## Verification

Test timeout, cancellation, invalid certificate or endpoint, non-success status, malformed response, oversized response, duplicate callback, stale result, and service outage.
