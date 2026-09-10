---
id: tools-module
type: module-design
title: Tools Module
status: active
parent: yogurt-arena-architecture
tags: [imported]
---

# Tools Module

The Tools module owns Unity integration helpers that are shared across gameplay modules. It includes GameObject-to-entity linking, prefab asset spawning, pooling, wait helpers, entity run loops, blueprint population, raycast-to-entity helpers, and small component/enum utilities.

## Boundary

Tools should stay generic. It may know about Yogurt, Unity, UniTask, assets, and pooling. It should not encode arena gameplay rules, agent policy, item behavior, or scenario progression.

## Decisions

`EntityLink` is the bridge between Unity object lifetime and ECS entity lifetime. Linked GameObjects are released to a pool when possible or destroyed when the entity dies. Wait helpers centralize app/game cancellation so async jobs do not need to duplicate cancellation wiring.
