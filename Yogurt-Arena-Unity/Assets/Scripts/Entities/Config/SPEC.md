---
id: config-module
type: module-design
title: Config Module
status: draft
parent: yogurt-arena-architecture
tags:
  - imported
---

# Config Module

The Config module turns Unity ScriptableObject authoring data into runtime ECS config entities. `LoadConfigsJob` reads resources, applies `EntityBlueprint` data, and makes configs queryable by type, level, or item kind.

## Boundary

The module owns loading and lookup of config components. It does not decide gameplay behavior. Factories in other modules ask it for data, then attach the selected config to newly created entities.

## Decisions

Config lookup is global-query based instead of dependency-injected. This keeps factories small, but it makes game startup order important: config loading must run before world factories request configs.
