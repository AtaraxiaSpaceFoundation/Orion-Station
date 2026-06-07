# Permission review

For every privileged entry point answer:

- where is authorization enforced;
- which permission is required;
- can the operation be triggered through another path;
- what happens when the actor or target disconnects;
- which audit record is produced;
- whether the operation can be repeated safely;
- whether server state remains consistent after failure.

A hidden UI control is not a security boundary.
