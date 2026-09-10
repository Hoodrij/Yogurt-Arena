---
id: yogurt-arena-architecture
type: architecture-design
title: Yogurt Arena Architecture
status: active
parent: yogurt-arena-goal
tags: [imported]
---

# Yogurt Arena Architecture

Yogurt Arena is organized as an ECS-like Unity game. Entities hold data components. Aspects provide typed access to common component sets. Jobs are the unit of game logic. Unity objects enter the model through `EntityLink`, pooled assets, and ScriptableObject config.

## Topology

`Boot` creates the game entity, then starts the game loop. The game loop creates a world entity, starts the scenario, waits for game over, shows restart UI, and kills the world so linked objects and child entities clean up.

The main runtime boundaries are:

- `game-module`: owns the application-level entity, frame time, and repeated loop.
- `config-module`: loads ScriptableObject configs into ECS entities and serves runtime lookups.
- `world-module`: owns one match session and creates world-scoped systems and views.
- `agent-module`: owns generic movable combatants.
- `player-module`: specializes an agent for user control.
- `overmind-module`: spawns and directs enemies.
- `items-module`: owns inventory, weapons, projectiles, pickups, and item use.
- `scenario-module`: sequences progression, quests, level-up, and game-over handling.
- `ui-module`: owns screen and world-space widgets.
- `tools-module`: owns bridges to Unity assets, pooling, waits, lifetimes, and shared helpers.

## Dependency rules

Factories can compose lower-level modules when they create a larger runtime scope. Behavior jobs should stay bound to an entity or aspect lifetime and use Yogurt wait helpers instead of blocking loops.

Shared gameplay state flows through components and queries, not through service singletons. The main exception is config lookup: config entities are loaded once by the game and queried by factories at runtime.

Module dependency edges observed in code:

- Game creates Config and World, then delegates progression to Scenario.
- World creates Location, UI, input, camera, player, enemy overmind, beacon, and item spawner entities.
- Player and Overmind both depend on Agent.
- Items depend on Agent, Health, Body, Config, Physics helpers, and Tools.
- Scenario depends on World, Player, Overmind, Items, Level, and UI.
- UI can observe Health but must not own health rules.

## Invariants

- Components are data, tags, views, or config only. Game behavior lives in jobs.
- Jobs expose a single public `Run(...)` entry.
- Long-lived behavior runs through `entity.Run(...)` or `aspect.Run(...)`, so work stops when the entity dies.
- GameObjects linked to entities are disposed through `EntityLink`; gameplay code kills entities instead of destroying linked objects directly.
- The world is the reset boundary for a match. Killing it should clean up its children and linked views.
- `BattleState` can be shared between an agent and its active weapon. Targeting semantics rely on that shared component instead of copied target state.

## Import notes

These specs were reverse-engineered from code and project instructions. They remain draft until reviewed. No durable design documents were found to adopt as spec nodes.
