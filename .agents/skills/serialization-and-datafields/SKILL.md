---
name: serialization-and-datafields
description: Design prototype DataFields, generated component state, custom serializers, and compatibility-safe data contracts.
---

# Serialization And DataFields

Serialized data is a compatibility surface shared by prototypes, maps, network state, configuration, or saves.

## Prototype data

Use `[DataField]` only for values content authors should configure. Choose defaults that produce a valid component before prototype application. Prefer typed prototype IDs, time spans, flags, and structured data over ad hoc strings.

Do not expose runtime caches, service references, or derived state as prototype fields. Validate required fields at prototype load or component startup using existing project patterns.

## Network state

Use the current generated state attributes for fields required by clients. Keep hidden values server-only. After authoritative mutation, dirty the component or generated field.

## Compatibility

Renaming a field can break maps and prototypes. Removing a field can break older content. Search YAML, maps, and downstream modules before changing names or types. Add migration or compatibility handling when the data persists.

## Custom serialization

Write a custom serializer only when standard serializers and structured data cannot express the format. Include round-trip and invalid-input tests.
