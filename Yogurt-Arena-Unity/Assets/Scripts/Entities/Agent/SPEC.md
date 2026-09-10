---
id: agent-module
type: module-design
title: Agent Module
status: active
parent: yogurt-arena-architecture
depends-on: [config-module]
tags: [imported]
---

# Agent Module

The Agent module owns generic movable combatants. An agent combines identity, body state, health, inventory, battle state, config, and view components. Spawn and behavior jobs create the view, link it to the entity, start movement, start look behavior, and equip default items.

## Boundary

Agent owns movement, facing, spawn setup, and death presentation for a generic combatant. It must not decide whether an agent is a player or enemy. Player and enemy behavior belong to `player-module` and `overmind-module`.

## Decisions

Navigation uses Unity NavMesh data through movement jobs. Combat targeting state is exposed through shared components so weapons and owners can coordinate without a controller service.
