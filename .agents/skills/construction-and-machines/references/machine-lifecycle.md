# Machine lifecycle

Review:

`prototype/map spawn -> initialization -> anchoring -> power -> user interaction -> part change -> damage/breakage -> deconstruction -> deletion`

At every transition, identify which derived values and UI states need recalculation. Prefer event-driven invalidation over repeated polling.
