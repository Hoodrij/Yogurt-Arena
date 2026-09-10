---
id: overmind-module
type: module-design
title: Overmind Module
status: active
parent: yogurt-arena-architecture
depends-on: [agent-module, player-module, ui-module, items-spawner-module]
tags: [imported]
---

# Overmind Module

The Overmind module owns enemy wave behavior. It creates enemy agents, assigns world-space health widgets, chooses spawn points away from the player, and updates enemy destinations toward targets or wander points.

## Boundary

Overmind may spawn and steer enemy agents and request their drops from Items Spawner. It should not own generic agent movement, item selection, pickup behavior, health rules, or scenario rewards. Scenario starts or gates overmind behavior when progression requires it.

## Decisions

Enemy spawning is tied to world and location state, not to individual enemy entities. The spawner samples valid NavMesh positions and filters them against player position. Enemy-only death behavior belongs here, so it delegates the generic death presentation to Agent and the directed pickup to Items Spawner.
