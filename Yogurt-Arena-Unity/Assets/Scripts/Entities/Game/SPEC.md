---
id: game-module
type: module-design
title: Game Module
status: active
parent: yogurt-arena-architecture
tags: [imported]
---

# Game Module

The Game module owns the application-level ECS root. `GameFactoryJob` creates the game entity, adds the frame `Time`, starts time updates, and loads config entities.

`RunGameLoopJob` owns the repeated match loop. It creates a world, starts the scenario, waits for game over, handles restart UI, then lets the loop create the next world.

## Boundary

The module may create the config registry and world sessions. It may call scenario orchestration jobs. It should not own combat, input, item, enemy, or UI rules beyond starting and resetting their scopes.

## Decisions

The game entity is long-lived across match restarts. The world entity is short-lived and acts as the reset boundary for one run.
