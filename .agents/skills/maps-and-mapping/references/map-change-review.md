# Map change review

Review map diffs for:

- intended entity additions/removals;
- unexpected coordinate or rotation changes;
- prototype availability;
- anchoring and parent relationships;
- container membership;
- grid and map metadata;
- serializer-wide reorder noise;
- source-fork-only IDs;
- required module resources.

A small gameplay change should not silently rewrite thousands of unrelated map lines.
