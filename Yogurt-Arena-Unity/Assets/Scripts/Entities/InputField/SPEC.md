---
id: input-field-module
type: module-design
status: active
title: Input Field Module
parent: yogurt-arena-architecture
depends-on: [config-module, camera-module]
tags: [documented]
---

# Input Field Module

## Responsibility

Input Field owns the input view and turns a screen click into one world-space click state. It uses the active gameplay camera to raycast against the world, with a ground-plane fallback.

## Boundary

Input Field reports intent only. It does not validate navigation, move the player, select targets, or render destination feedback. Beacon consumes its click state and decides the reachable destination.

## Decision

The click is stored as transient ECS state rather than sent through a callback. This lets the input reader and destination behavior remain lifetime-bound jobs.
