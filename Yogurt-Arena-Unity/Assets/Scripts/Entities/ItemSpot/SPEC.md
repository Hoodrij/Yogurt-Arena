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

Item Spot owns a world pickup location, its availability state, and its view lifecycle. An active spot displays its item, waits for an agent pickup, grants the item, and hides the view. Normal spots then become available again; transient drops end their entity lifetime so their pooled views return to the pool.

## Boundary

Item Spot does not choose which items appear or maintain the global pickup count. Items Spawner activates free spots; Inventory grants the selected item.

## Decision

A normal spot becomes reusable only after its pickup flow completes and its short cooldown expires. This separates local presentation and collection from global spawning policy. A transient marker selects disposal instead, preventing one-time drops from becoming normal spawn locations.
