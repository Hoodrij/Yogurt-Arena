---
id: inventory-module
type: module-design
status: active
title: Inventory Module
parent: yogurt-arena-architecture
depends-on: [items-module, agent-module]
tags: [documented]
---

# Inventory Module

## Responsibility

Inventory records an agent's active weapon. `GiveItemJob` creates an item for an agent, replaces any previous weapon when the new item is a weapon, and starts the item-use behavior.

## Boundary

Inventory owns assignment and replacement of equipped items. Items owns item creation and use behavior; Agent owns the combatant that holds the inventory.

## Decision

An active weapon is an entity reference. Replacing it kills the old weapon entity so ownership and linked cleanup remain explicit.
