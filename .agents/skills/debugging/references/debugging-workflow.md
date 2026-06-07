# Debugging workflow

Start with logs and code tracing, then use breakpoints or View Variables when runtime state is uncertain.

For desyncs, inspect the same entity on server and client, compare networked fields, and test with added latency.

For UI bugs, trace server state generation, client state application, event subscriptions, and window lifecycle.

For prototype failures, inspect linter output and resolve the first schema or reference error before later cascades.
