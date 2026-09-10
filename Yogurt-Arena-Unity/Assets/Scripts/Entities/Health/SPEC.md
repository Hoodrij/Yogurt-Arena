---
id: health-module
type: module-design
status: active
title: Health Module
parent: yogurt-arena-architecture
depends-on: [ui-module]
tags: [documented]
---

# Health Module

## Responsibility

Health owns hit-point changes, area damage, and death transition for entities with `Health`. A change clamps health, requests the current widget update, runs the configured death job, and kills a depleted entity.

## Boundary

Health accepts damage and healing requests but does not select targets, define weapon effects, or own visual presentation. Combat and item modules choose when to deal damage; UI presents the resulting state.

## Decision

Death is an entity-lifetime transition. Health invokes the configured death effect before killing the entity so linked views and child entities use the standard cleanup path.
