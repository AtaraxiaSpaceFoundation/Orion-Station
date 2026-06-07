---
name: maps-and-mapping
description: Edit map files, map prototypes, grids, placements, entity references, and map compatibility safely.
---

# Maps And Mapping

Maps are serialized compatibility artifacts, not ordinary hand-written YAML.

## Workflow

1. Identify whether the change belongs in a map file, map prototype, dungeon/template resource, or runtime spawner.
2. Use the supported map editor or serializer path where possible.
3. Keep module-owned entities and prototypes available in the map's runtime module set.
4. Review entity parents, grids, coordinates, anchored state, containers, and map IDs.
5. Avoid unrelated serializer churn.
6. Validate map schemas and load the affected map.

## Porting

Do not copy map chunks from another fork before verifying every prototype ID, component field, tile, and resource. Replace unavailable entities deliberately and record behavior changes.

## Review failures

- missing prototype referenced by map entity;
- accidental mass reserialization;
- module resource unavailable at map load;
- entity inside invalid container or parent;
- orphaned grid or map entity;
- changed GUID or map metadata without reason.

## Verification

Run map schema validation, map loading or integration coverage, and inspect the diff for unrelated generated changes.
