# Service failure matrix

Define behavior for:

- DNS or connection failure;
- timeout and cancellation;
- authentication failure;
- rate limiting;
- non-success status;
- malformed or incomplete payload;
- duplicate response;
- stale response after state changed;
- service unavailable for the whole round;
- shutdown while a request is pending.

The game should fail predictably without blocking the simulation thread or applying stale results.
