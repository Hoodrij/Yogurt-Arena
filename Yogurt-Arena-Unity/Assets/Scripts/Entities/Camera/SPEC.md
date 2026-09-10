---
id: camera-module
type: module-design
status: active
title: Camera Module
parent: yogurt-arena-architecture
depends-on: [config-module, beacon-module]
tags: [documented]
---

# Camera Module

## Responsibility

Camera owns creation and smooth per-frame positioning of the gameplay camera. It frames the shared beacon destination and applies configured mouse influence while the player exists.

## Boundary

Camera may observe Beacon and Player state, but it does not translate input into destinations or move gameplay entities. Input Field uses the camera only to project screen clicks into world space.

## Decision

Camera smoothing uses game frame-time configuration for its target smoothness while Unity frame time drives the interpolation step.
