---
id: world-module
type: module-design
title: World Module
status: draft
parent: yogurt-arena-architecture
depends-on:
  - game-module
tags:
  - imported
---

# World Module

The World module owns one playable match session. `WorldFactoryJob` creates the world entity and level state, then composes the scene-level systems: location, UI, world UI, input field, beacon, camera, player, overmind, and item spawner.

## Boundary

World may compose modules that must share a match lifetime. It should not contain the behavior rules of those modules. Child entities and linked GameObjects should be parented to the world or to a world-owned entity so game-over cleanup is centralized.

## Decisions

The world is the runtime lifetime boundary below the game. Scenario and game-over handling can kill it without killing the game entity or config registry.
