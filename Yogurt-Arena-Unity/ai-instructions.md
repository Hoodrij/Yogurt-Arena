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

### Queries
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

# Layer 2 - Yogurt Extensions 