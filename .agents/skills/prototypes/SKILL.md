---
name: prototypes
description: Create, inherit, compose, localize, and validate YAML prototypes in the correct resource root.
---

# Prototypes

## Workflow

1. Find a nearby prototype of the same type and owner.
2. Confirm the owning resource root from `module.yml`.
3. Choose inheritance or composition instead of copying a large prototype.
4. Use stable, specific IDs and typed IDs in code.
5. Add required component data and locale.
6. Verify every referenced prototype, sprite, state, sound, and dataset.
7. Run the YAML linter.

Keep serialized field names consistent with the component schema. Do not invent fields based on another fork without checking the destination component.

A port should adapt prototype inheritance and IDs to the destination tree. Copying source YAML without checking removed components, renamed fields, and changed parents is not a valid port.
