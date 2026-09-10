---
id: item-spot-module
type: module-design
status: active
title: Item Spot Module
parent: yogurt-arena-architecture
depends-on: [inventory-module, config-module]
tags: [documented]
---

# Item Spot Module

## Responsibility

Item Spot owns a world pickup location, its availability state, and its view lifecycle. An active spot displays its item, waits for an agent pickup, grants the item, hides the view, then becomes available again.

## Boundary

Item Spot does not choose which items appear or maintain the global pickup count. Items Spawner activates free spots; Inventory grants the selected item.

## Decision

A spot becomes reusable only after its pickup flow completes and its short cooldown expires. This separates local presentation and collection from global spawning policy.
