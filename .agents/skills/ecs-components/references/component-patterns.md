# Component patterns

Prefer small components grouped by responsibility rather than one feature-wide state bag.

Configuration fields should be prototype-driven when content authors need tuning. Runtime-only fields should not be exposed as data fields without a reason.

Optional relationships should use nullable entity references or the established network-safe representation. Resolve components in systems, not by storing direct component references in serialized state.

When a field changes on the server and the client consumes it, dirty the component or specific field according to the generated state pattern used nearby.
