---
name: containers-and-inventory
description: Move entities through containers, hands, slots, storage, equipment, and inventories without corrupting ownership or transforms.
---

# Containers And Inventory

Containers and inventories define entity ownership, visibility, transform parenting, and interaction availability.

## Workflow

1. Identify the owning container, slot, hand, or storage system.
2. Use the established insertion, removal, pickup, drop, and equip APIs.
3. Validate capacity, whitelist, blacklist, access, body state, and current ownership.
4. Handle insertion/removal events and side effects once.
5. Preserve transform and PVS behavior.
6. Update UI state after authoritative inventory changes.

## Rules

Do not manually parent an entity to simulate container insertion. Do not directly mutate container collections. Use system APIs so lifecycle events, networking, and transform state remain consistent.

For hands and equipment, account for active hand, blocked slots, nested storage, item deletion, body changes, polymorph, and disconnection. Server validation owns protected moves; client drag-and-drop is only an intent.

## Common failures

- entity exists in two containers;
- item is parented correctly but not registered in the container;
- removal event is skipped;
- PVS leaks contained entities;
- storage UI shows stale contents;
- deletion leaves an invalid slot reference;
- hand transfer duplicates predicted feedback.

## Verification

Test insert, remove, swap, drop, delete, nested storage, full inventory, denied whitelist, body transfer, reconnect, and multiple viewers.
