# Network serialization

Network payloads and component states must use supported serializable types. Convert entity references through the current `NetEntity` APIs. Avoid sending large components, services, prototype objects, or arbitrary dictionaries when a small state record is sufficient.

Review compatibility when changing message shape. Both sides must agree on field order and types. Integration tests should cover missing entities, stale messages, and state received after the target was deleted.
