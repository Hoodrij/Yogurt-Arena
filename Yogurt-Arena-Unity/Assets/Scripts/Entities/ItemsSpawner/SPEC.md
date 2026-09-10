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

Items Spawner maintains the configured number of occupied normal item spots in the active world. It waits for capacity, selects a free spot, then selects an allowed random item type for that spot. It also creates directed transient pickups, including enemy drops, at supplied world positions using the same active item pool.

## Boundary

Items Spawner controls normal world pickup availability and directed transient pickup creation. It does not display a spot, grant an item, or decide scenario progression.

## Decision

Spawn capacity is derived from the current normal Item Spot query rather than retained as a parallel counter, so spot cleanup automatically returns capacity. Transient drops are excluded from that query so they do not affect normal pickup capacity.
