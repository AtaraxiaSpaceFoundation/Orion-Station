---
name: chemistry-and-reagents
description: Modify reagents, solutions, reactions, transfers, metabolism, effects, containers, and chemistry UI safely.
---

# Chemistry And Reagents

Chemistry is data-driven and frequently crosses prototypes, server simulation, Shared solution state, UI, and localized effects.

## Reagents and reactions

Use existing reagent and reaction prototype types. Verify IDs, quantities, catalysts, temperature or condition requirements, products, and side effects. Keep balance data in prototypes rather than hardcoded branches where possible.

## Solutions and transfer

Use solution-system APIs for adding, removing, splitting, transferring, and querying reagents. Do not mutate solution collections directly. Define behavior for insufficient volume, capacity, spills, deleted containers, and concurrent transfers.

## Authority and prediction

The server owns protected reaction outcomes, metabolism, damage, and inventory changes. Client UI may preview known data but must not choose authoritative results.

## Effects

Reagent effects need clear conditions, scaling, intervals, and maximum behavior. Avoid per-tick allocations and repeated expensive lookups in metabolism paths.

## UI and localization

Keep reagent names, descriptions, alerts, and machine controls localized. Do not expose internal reagent IDs as display text.

## Verification

Test exact quantities, boundary volumes, reaction order, catalysts, temperature conditions, repeated metabolism, transfer cancellation, and YAML validation.
