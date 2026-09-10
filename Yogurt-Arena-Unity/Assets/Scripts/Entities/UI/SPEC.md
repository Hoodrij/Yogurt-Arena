---
id: ui-module
type: module-design
title: UI Module
status: draft
parent: yogurt-arena-architecture
depends-on:
  - config-module
tags:
  - imported
---

# UI Module

The UI module owns screen-space and world-space presentation widgets. It creates the main UI view, world UI view, health widgets, and game-over widget. Health systems call UI jobs to bind or update widgets.

## Boundary

UI may display state from gameplay components. It should not decide health, combat, scenario, input, or restart rules. Gameplay modules can request widgets or updates, but UI remains presentation.

## Decisions

World health bars are separate entities/views linked to targets. This keeps presentation lifetime explicit and lets health logic update widgets without owning their prefab lifecycle.
