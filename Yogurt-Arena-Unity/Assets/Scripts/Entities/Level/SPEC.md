---
id: level-module
type: module-design
status: active
title: Level Module
parent: yogurt-arena-architecture
depends-on: [location-module]
tags: [documented]
---

# Level Module

## Responsibility

Level owns the current level index for a world. A level-up requests the next location part and advances the index only after that part is available.

## Boundary

Level decides neither the trigger for progression nor the assets used for a location part. Scenario requests level-up; Location selects and creates the part.

## Decision

Location creation precedes index mutation. This keeps the stored level equal to the highest successfully added location part.
