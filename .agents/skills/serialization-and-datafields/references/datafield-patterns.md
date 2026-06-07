# DataField patterns

Review every field for:

- who configures it;
- whether a default is safe;
- whether null is meaningful;
- whether a typed ID exists;
- whether the value is runtime-only or derived;
- whether maps or prototypes already serialize the old name;
- whether validation belongs at prototype load or runtime.

Collections need an explicit replacement or merge expectation. Mutable reference defaults must not be shared unintentionally between component instances.
