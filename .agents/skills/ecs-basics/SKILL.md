---
name: ecs-basics
description: Read and reason about an unfamiliar SS14 ECS subsystem before changing it.
---

# ECS Basics

Use this when entering an unfamiliar feature or reviewing a cross-system bug.

## Reading order

1. Find the primary component and prototype that adds it.
2. Find systems subscribing to that component's lifecycle and events.
3. Trace the player or simulation entry point.
4. Follow validation, mutation, dirtying, and presentation.
5. Identify Server, Shared, Client, and Resources ownership.
6. Find focused or integration tests.

Entities are identifiers. Components hold data. Systems own behavior. Prototypes compose initial entity state. Events connect established flows. Resources provide data and presentation.

## Questions

- Which component represents the state?
- Which system is allowed to mutate it?
- Which event begins the flow?
- What is authoritative?
- What is predicted or replicated?
- What lifecycle event initializes or cleans up the feature?
- Which prototype or resource references are required?

Do not begin by copying a nearby system until you understand why its ownership and lifecycle match the new task.
