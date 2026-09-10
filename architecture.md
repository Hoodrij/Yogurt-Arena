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
- `health-module`: owns health changes, death transition, and area damage.
- `input-field-module`: turns screen clicks into world-space input state.
- `beacon-module`: owns the shared reachable player destination and its feedback.
- `camera-module`: owns gameplay camera creation and framing.
- `player-module`: specializes an agent for user control.
- `overmind-module`: spawns and directs enemies.
- `inventory-module`: owns active-weapon assignment for agents.
- `items-module`: owns items, weapons, projectiles, and item-use behavior.
- `item-spot-module`: owns individual pickup locations and collection flow.
- `items-spawner-module`: maintains world pickup availability.
- `location-module`: owns location geometry and NavMesh composition.
- `level-module`: owns the current world level index.
- `scenario-module`: sequences progression, quests, level-up, and game-over handling.
- `ui-module`: owns screen and world-space widgets.
- `tools-module`: owns bridges to Unity assets, pooling, waits, lifetimes, and shared helpers.

## Dependency rules

Factories can compose lower-level modules when they create a larger runtime scope. Behavior jobs should stay bound to an entity or aspect lifetime and use Yogurt wait helpers instead of blocking loops.

Shared gameplay state flows through components and queries, not through service singletons. The main exception is config lookup: config entities are loaded once by the game and queried by factories at runtime.

Module dependency edges observed in code:

- Game creates Config and World, then delegates progression to Scenario.
- World creates Location, UI, Input Field, Beacon, Camera, Player, Overmind, and Items Spawner entities.
- Input Field provides screen-to-world intent to Beacon; Beacon provides the destination observed by Player and Camera.
- Player and Overmind both create or direct Agent entities. Agent delegates default equipment to Inventory; Items creates the equipped item behavior.
- Items Spawner activates normal Item Spots and creates directed transient drops; Item Spots delegate grants to Inventory. Items supplies valid item types and use behavior. Overmind requests enemy drops from Items Spawner.
- Scenario requests Level progression; Level requests the next Location part. Location rebuilds navigation after composition.
- Health requests UI updates and performs death transition; UI can observe Health but must not own health rules.

## Invariants

- Components are data, tags, views, or config only. Game behavior lives in jobs.
- Jobs expose a single public `Run(...)` entry.
- Long-lived behavior runs through `entity.Run(...)` or `aspect.Run(...)`, so work stops when the entity dies.
- GameObjects linked to entities are disposed through `EntityLink`; gameplay code kills entities instead of destroying linked objects directly.
- The world is the reset boundary for a match. Killing it should clean up its children and linked views.
- `BattleState` can be shared between an agent and its active weapon. Targeting semantics rely on that shared component instead of copied target state.
- `BodyState` and `CollisionInfo` are shared data primitives. They carry spatial or collision data only; no standalone module owns behavior for them.
