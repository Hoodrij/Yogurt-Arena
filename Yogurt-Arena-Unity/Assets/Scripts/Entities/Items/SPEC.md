---
id: items-module
type: module-design
title: Items Module
status: active
parent: yogurt-arena-architecture
depends-on: [agent-module, config-module, health-module]
tags: [imported]
---

# Items Module

The Items module owns item creation, weapon use, projectiles, and item lifetimes. Item factories select ScriptableObject-backed configs, attach item ownership, parent items to their owner, and start the configured item-use job.

## Boundary

Items may read agent body, health, battle state, config, and physics data. Items may deal damage through health jobs. They should not own player input, enemy wave policy, or scenario progression.

## Decisions

Weapons are modeled as entities owned by agents. Shared `BattleState` lets target detection and firing jobs coordinate between owner and weapon. Inventory owns active-weapon assignment; Item Spot and Items Spawner own pickup collection and availability.
