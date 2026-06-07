# Regression test design

A regression test should contain the smallest setup that reproduces the old failure and an assertion that distinguishes fixed from broken behavior.

Good additional cases include:

- lower and upper boundaries;
- repeated call;
- rejected permission;
- entity deletion;
- cancellation;
- reconnect or late state;
- invalid prototype reference;
- stale client state;
- provider-specific database behavior.
