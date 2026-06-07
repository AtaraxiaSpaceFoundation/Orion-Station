---
name: transform-and-physics
description: Work with maps, grids, coordinates, anchoring, movement, fixtures, collisions, and physics safely.
---

# Transform And Physics

Use the coordinate and transform APIs appropriate to the operation. Do not manually mix local, grid, entity, and map coordinates without explicit conversion.

## Transform workflow

1. Identify the current parent, grid, and map.
2. Use the transform system for movement, parenting, anchoring, and coordinate conversion.
3. Preserve attachment semantics when spawning or moving entities.
4. Account for containers, deletion, and map transfer.
5. Avoid direct transform-component mutation when a system API exists.

## Physics workflow

Inspect body type, fixtures, collision layers, masks, density, awake state, and contact events. Keep authoritative collision changes on the server/shared path used nearby. Update fixture state through established physics APIs.

## Common failures

- using map coordinates where local coordinates are required;
- detaching an entity unintentionally;
- moving an entity while inside a container;
- deleting during an unsafe enumeration;
- changing only client collision visuals;
- forgetting grid or parent changes.

## Verification

Test grids, map edges, anchoring, unanchored movement, containers, collisions, fixture updates, and deletion during events.
