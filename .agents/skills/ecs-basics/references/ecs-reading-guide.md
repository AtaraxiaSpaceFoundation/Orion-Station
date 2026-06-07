# ECS reading guide

Search by component type, event type, prototype ID, and user-visible locale key. These searches reveal different parts of the same feature.

Build a small flow map:

`prototype -> component -> subscription -> validation -> mutation -> dirtying -> client visual/UI -> tests`

Mark each arrow with its assembly. Unexpected Server-to-Client shortcuts or Shared references to client-only types usually indicate an ownership problem.
