---
id: location-module
type: module-design
status: active
title: Location Module
parent: yogurt-arena-architecture
depends-on: [config-module, tools-module]
tags: [documented]
---

# Location Module

## Responsibility

Location owns the world level root, its NavMesh surface, and spawning of configured location parts. It links spawned parts to the location lifetime, animates later parts, and rebuilds the NavMesh after each addition.

## Boundary

Location composes playable geometry but does not decide when the level advances, place enemies, or move agents. Level requests new parts; Overmind and Agent use the resulting NavMesh.

## Decision

Location parts are children of the location entity and Unity transform. Killing the world therefore removes geometry and navigation state with the match.
