# Solution safety

Before moving reagents, confirm:

- source and destination still exist;
- source solution is current;
- destination capacity is sufficient or partial transfer is intended;
- quantity is positive and bounded;
- operation is authoritative;
- reaction processing occurs through established APIs;
- UI state refreshes after mutation.

Do not calculate a transfer from stale client state and then apply it without recomputing on the server.
