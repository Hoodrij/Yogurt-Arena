---
id: beacon-module
type: module-design
status: active
title: Beacon Module
parent: yogurt-arena-architecture
depends-on: [config-module, input-field-module]
tags: [documented]
---

# Beacon Module

## Responsibility

Beacon owns the shared player destination. It consumes world-space click state, finds a complete NavMesh path, stores the reachable destination and raw target, and spawns destination feedback.

## Boundary

Beacon does not read movement controls directly or move the player. Player and Camera observe beacon state for navigation and framing.

## Decision

The destination is indirect: input updates Beacon, then consumers use the same state. This keeps visual feedback, player movement, and camera targeting aligned.
