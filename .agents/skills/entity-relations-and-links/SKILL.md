---
name: entity-relations-and-links
description: Maintain entity references, ownership links, target relationships, deletion cleanup, and network-safe relation state.
---

# Entity Relations And Links

Entity references are temporary relationships, not permanent object references.

## Storage

Use nullable entity IDs, network-safe entity forms, or established relation components. Do not serialize direct component references. Define whether the relation is one-way, bidirectional, unique, or many-to-many.

## Lifecycle

Handle target deletion, source deletion, map transfer, container changes, body transfer, and component removal. Subscribe to relation-breaking events or revalidate before every use according to the lifetime and performance needs.

## Ownership and authority

The server owns protected relationships such as account ownership, machine control, role assignment, or target locks. Clients may receive only the relation data needed for presentation.

## Consistency

If both entities store the relationship, update both sides atomically or through one owning system. Prevent duplicate links and stale back-references.

## Common failures

- deleted target remains cached;
- entity transfer preserves a relation that should reset;
- two systems each believe they own cleanup;
- client supplies an arbitrary linked entity;
- network state exposes hidden targets;
- save data restores an invalid entity ID.

## Verification

Test link, relink, unlink, source deletion, target deletion, map transfer, reconnect, duplicate request, and invalid restored state.
