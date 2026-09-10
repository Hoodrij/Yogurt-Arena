---
id: player-module
type: module-design
title: Player Module
status: draft
parent: yogurt-arena-architecture
depends-on:
  - agent-module
  - ui-module
tags:
  - imported
---

# Player Module

The Player module specializes an agent for user control. It creates the player agent, adds the player tag, attaches the main health widget, and starts the behavior that copies beacon destinations into the player's body state.

## Boundary

Player may depend on Agent, Beacon, and UI. It should not own generic movement mechanics, weapon behavior, enemy spawning, or scenario progression.

## Decisions

Player destination is indirect. Input updates a beacon, and the player follows that beacon destination. This lets camera follow and visual feedback share the same navigation target.
