AI Instructions

# Layer 1 - Yogurt

### 🏷️ Entity
Entities are lightweight containers that hold arbitrary components. They form the core of the ECS-like architecture.

```csharp
Entity entity = Entity.Create();

Assert.IsTrue(entity != Entity.Null);
Assert.IsTrue(entity != default);
Assert.IsTrue(entity.Exist);

entity.Kill();

Assert.IsFalse(entity.Exist);
```

### 🏷️ Component
Components should not contain any logic. They are data storages, tags or configs.
All components implement `IComponent` interface. They could be pure C# classes, structs, MonoBehaviors or ScriptableObjects

```csharp
public class Health : IComponent
{
    public int Value;
}
public class PlayerView : MonoBehavior, IComponent
{
    public void Animate() { }
}
public class WeaponConfig : ScriptableObject, IComponent
{
    public float reloadTime;
}
```

Entity has a bunch of methods to operate with components.

```csharp
entity.Add(new Health());
entity.Set(new Health());
entity.Has<Health>();
entity.Remove<Health>();

entity.Get<Health>();
entity.TryGet(out Health health);
```

### 🏷️ Aspect

Aspect is an Entity with a defined set of Components. Used to speed up the interaction with Entity.

```csharp
public struct PlayerAspect : IAspect
{
    public Entity Entity { get; set; }

    public PlayerTag Tag => this.Get<PlayerTag>();
    public PlayerConfig Config => this.Get<PlayerConfig>();
    public Health Health => this.Get<Health>();
    public Body Body => this.Get<Body>();

    public NestedAspect NestedAspect => this.Get<NestedAspect>();
}

// usage
PlayerAspect player = entity.As<PlayerAspect>();
bool isAlive = player.Health.Value > 0;
player.Add(new OtherComponent());
player.Exist();
player.Kill();
```

### 🏷️ Query

Query is used to get required Entities.

- Getting a Query

    ```csharp
    // Query of an Entity
    var query = Query.Of<Health>()
                     .With<PlayerTag>()
                     .Without<DeadTag>();

    // Or Query of an Aspect
    var query = Query.Of<PlayerAspect>()
                     .Without<DeadTag>();
    ```

- Operating with Query

    ```csharp
    // Iterate over
    foreach (Entity entity in query)
    {

    }

    // Or get Single
    Entity entity = query.Single();

    // Common LINQ methods
    query.Where(entity => entity.Get<Health>().Value > 50)
         .Any();
    ```

- Fast Single Query

```csharp
// Of Component
GameData data = Query.Single<GameData>();
// same as
GameData data = Query.Of<GameData>().Single().Get<GameData>()

// Or of an Aspect
PlayerAspect playerAspect = Query.Single<PlayerAspect>();
```

### 🏷️ Entity hierarchy

Entity provides few methods to combine them into a Parent-Child relationship. All Childs will be killed after a Parent death.

```csharp
entity.SetParent(parentEntity);
entity.UnParent();
```

### 🏷️ Complete usage Patterns

#### Entity Creation
```csharp
Entity player = Entity.Create()
    .Add(new PlayerTag())
    .Add(new PlayerConfig())
    .Add(new Body())
    .Add(new Health())
    .SetParent(worldEntity);
```

#### Component Access
```csharp
// Direct access
Body body = entity.Get<Body>();
body.Position = Vector3.zero;

// Via aspects
AgentAspect agent = entity.As<PlayerAspect>();
agent.Body.Position = Vector3.zero;
```

#### Queries
```csharp
// Find all enemies
foreach (AgentAspect enemy in Query.Of<AgentAspect>().Without<PlayerTag>())
{
    enemy.Body.Destination = playerPosition;
}

// Get singletons
Time gameTime = Query.Single<Time>();
PlayerAspect player = Query.Single<PlayerAspect>();
```

# Layer 2 - Yogurt ↔ Unity Integration

### Lifetime (concept)
Lifetime is an awaitable, disposable cancellation context used to scope async work. It wraps a CancellationTokenSource and provides:
- Implicit conversions to/from UniTask and CancellationToken
- Boolean checks: IsAlive/IsDead (via implicit bool)
- Composition operators:
  - life & life → end when BOTH life dies (WhenAll)
  - life | life → end when EITHER life dies (WhenAny)
- Parenting: life.SetParent(life) → life ends when parentTask completes

Use Lifetime to cleanly stop async loops when the Entity dies, the app quits, or another awaited task finishes.

### Wait methods (Unity-friendly async)
- Wait.While(predicate, life?) → loops while predicate is true; auto-cancelled by app lifetime and optional life
- Wait.Until(predicate, life?) → resolve when predicate becomes true
- Wait.Update() → await next frame
- Wait.Seconds(f) → await seconds (frame-safe)
- Wait.Any(params UniTask[]) / Wait.All(params UniTask[])

Note: Wait internally attaches Application.exitCancellationToken so awaits are cancelled on app exit. When you pass a Lifetime, it combines with app lifetime.

### Yogurt extensions
- entity.Link(GameObject) → ensures EntityLink MonoBehaviour is present and bound to the Entity (auto cleanup)
- entity.Life() → returns a Lifetime that ends when the entity no longer exists
- entity.Run(Action) / entity.Run(Func<UniTask>) → per-frame loop while entity exists
- aspect.Life(), aspect.Run(...) → same, using aspect.Entity

### EntityLink (GameObject bridge)
- MonoBehaviour that implements IComponent and holds the bound Entity
- Set(Entity): binds to entity, adds itself as component, awaits entity.Life(), then Dispose()s (release to PoolLink or Destroy(gameObject))
- Clear(): removes EntityLink component from the entity. So GameObject could leave independently of Entity  

---

### Examples

1) Link a spawned GameObject to an Entity (auto-destroy with entity)
```csharp
Entity playerEntity = Entity.Create()
    .Link(playerView.gameObject)   // binds GameObject to Entity
    .Add(playerView);
```

2) Run per-frame logic while an Entity exists
```csharp
entity.Run(() => {
    // executes every frame while entity exist
    UpdateLoop();
});

entity.Run(async () => {
    await SomeAsyncStep();
});
```

3) Await entity death, or race with a timeout
```csharp
// Await entity death
await entity.Life();

// Race: either 2 seconds pass, or the entity dies
await Wait.Any(Wait.Seconds(2f), entity.Life());
```

4) Scope a Lifetime with AND/OR composition
```csharp
Lifetime life = entity.Life();
// End when BOTH the entity dies and SomeLongOp completes
Lifetime both = life & SomeLongOp();
// End when EITHER the entity dies OR 5 seconds pass
// Means its alive for a life scope, but no longer than 5 seconds
Lifetime any = life | Wait.Seconds(5f);

await Wait.While(() => StillActive(), any);
```

5) Use Lifetime in loops and cancellable waits
```csharp
Lifetime life = entity.Life();
await Wait.While(() => NotReadyYet(), life);   // stops when entity dies or app exits
await Wait.Until(() => Ready(), life);
await Wait.Update(); // next frame
```

6) Unlink or manually dispose linked GameObject
```csharp
// Clear the link but keep GameObject alive
var link = go.GetComponent<EntityLink>();
link.Clear();

await PlayAdditionalAnimation();
// Or explicitly dispose pooled/non-pooled GO via EntityLink
link.Dispose(); // releases PoolLink if present, else Destroy(gameObject)
```
