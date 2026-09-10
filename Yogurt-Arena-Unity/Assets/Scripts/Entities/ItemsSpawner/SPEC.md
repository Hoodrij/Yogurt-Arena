---
id: items-spawner-module
type: module-design
status: active
title: Items Spawner Module
parent: yogurt-arena-architecture
depends-on: [item-spot-module, items-module, config-module]
tags: [documented]
---

# Items Spawner Module

## Responsibility

Items Spawner maintains the configured number of occupied item spots in the active world. It waits for capacity, selects a free spot, then selects an allowed random item type for that spot.

## Boundary

Items Spawner controls only world pickup availability. It does not display a spot, grant an item, or decide scenario progression.

## Decision

Spawn capacity is derived from the current Item Spot query rather than retained as a parallel counter, so spot cleanup automatically returns capacity.
