---
id: scenario-module
type: module-design
title: Scenario Module
status: active
parent: yogurt-arena-architecture
depends-on: [world-module, agent-module, items-module, player-module, overmind-module, level-module, ui-module]
tags: [imported]
---

# Scenario Module

The Scenario module sequences one match's progression. It waits for pickup and kill quests, starts enemy pressure, levels up the location, waits for game over, shows restart UI, and ends the world.

## Boundary

Scenario may orchestrate other modules and await their conditions. It should not implement the low-level behavior it sequences. Per-frame movement, combat, input, UI rendering, and spawning mechanics stay in their owning modules.

## Decisions

Progression is implemented as async orchestration jobs instead of a central update state machine. Wait jobs return when world or player lifetimes end, so scenario flow remains bound to the active match.
