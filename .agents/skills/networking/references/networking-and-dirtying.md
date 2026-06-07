# Networking and dirtying reference

Typical review sequence:

1. Locate the shared payload or component.
2. Confirm serialization and entity conversion.
3. Locate sender and receiver.
4. Validate server-side trust checks.
5. Locate authoritative mutation.
6. Confirm component or field dirtying.
7. Locate client state application and presentation.
8. Test reconnection, late state, rejection, and repeated prediction where relevant.

Use field-level dirtying only when supported by the generated state and when it meaningfully reduces updates.
