---
id: yogurt-arena-goal
type: goal-and-requirements
title: Yogurt Arena Goal and Requirements
status: draft
tags:
  - imported
---

# Yogurt Arena Goal and Requirements

Yogurt Arena is a Unity sample game for the Yogurt ECS-style framework. It demonstrates an arena loop where a player-controlled agent collects weapons, fights waves of enemies, advances through level parts, and restarts after game over. This purpose is inferred from the README and code and is unconfirmed.

## Scope

- Provide a playable sample that shows Yogurt entities, components, aspects, queries, lifetimes, and jobs in a Unity project.
- Run a repeated match loop: create game state, create a world, run a scenario, wait for player death, show restart UI, and reset the world.
- Use ScriptableObject-backed config and prefab assets to keep gameplay data outside job code.
- Keep gameplay logic in small composable jobs. Components remain data, tags, or config objects.

## Non-goals

The code does not show multiplayer, persistence, editor tooling, or production live-service systems. Treat these as out of scope unless a reviewed requirement adds them. This is inferred and unconfirmed.

## Runtime stack

The project is a Unity/C# game. It depends on Yogurt, UniTask, Unity NavMesh, PrimeTween, UGUI, and pooled prefab spawning.
