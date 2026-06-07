# Visual lifecycle

Trace visual state through:

`component start -> appearance/state update -> client visual application -> repeated update -> component removal -> entity deletion`

For overlays and child visuals, add map transfer and client reconnect. Every allocated client visual object needs a clear owner and cleanup point.

Common leaks include duplicate event subscriptions, overlays added more than once, child entities never deleted, and shader instances retained after the feature closes.
