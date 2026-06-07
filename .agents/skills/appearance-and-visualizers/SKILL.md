---
name: appearance-and-visualizers
description: Implement replicated appearance data, GenericVisualizer mappings, sprite layers, overlays, and client visual systems.
---

# Appearance And Visualizers

Appearance data describes presentation state. It must not become a second authoritative gameplay model.

## Workflow

1. Identify the authoritative component and system.
2. Define shared appearance keys only for values the client needs.
3. Update appearance after authoritative or predicted state changes.
4. Map simple properties through `GenericVisualizer` or the nearby equivalent.
5. Use a client visual system for transitions, overlays, shaders, spawned visuals, or complex cleanup.
6. Verify sprite layers, RSI states, and default appearance.
7. Test initialization, late join, mutation, removal, and deletion.

## Boundaries

Keep gameplay checks and protected state out of the visual system. Client visuals consume replicated or predicted presentation data. Do not send large objects through appearance values.

## Lifecycle

Clean up overlays, child entities, animations, and subscriptions when the component shuts down. Avoid recreating layers every update when they can be reused.

## Common failures

- appearance not refreshed after mutation;
- wrong layer key;
- missing RSI state;
- client visual state surviving component removal;
- duplicated predicted animation;
- using appearance as authority instead of presentation.
