---
id: items-module
type: module-design
title: Items Module
status: active
parent: yogurt-arena-architecture
depends-on: [agent-module, config-module]
tags: [imported]
---

# Items Module

The Items module owns inventory, pickups, weapon use, projectiles, and item lifetimes. Item factories select ScriptableObject-backed configs, attach item ownership, parent items to their owner, and start the configured item-use job.

## Boundary

Items may read agent body, health, battle state, config, and physics data. Items may deal damage through health jobs. They should not own player input, enemy wave policy, or scenario progression.

## Decisions

Weapons are modeled as entities owned by agents. Shared `BattleState` lets target detection and firing jobs coordinate between owner and weapon. Pickup spots and item spawning are world-scoped economy behavior but remain in this module because they create and grant items.
