---
apply: always
---

# Lifetime - Process State Representation

## Philosophy

`Lifetime` is a semantic representation of whether a process is currently happening. It answers the fundamental question: **"Is this process happening right now?"**

The core philosophy is:
- **Process representation**: A `Lifetime` represents a process
- **Binary state**: Processes are either happening (alive) or not happening (dead)
- **Immutable state transition**: Once a process ends (dies), it doesn't restart - it stays ended
- **Semantic clarity**: "Is the effect alive?" is clearer than "Is the token not cancelled?"
- **Composability**: Combine multiple process lifetimes using logical operators
- **Integration**: Seamless interop with UniTask and Unity's MonoBehaviour lifecycle

## Core Concept

A `Lifetime` represents **whether a process is currently active**. It has two states:
- **Alive**: The process is happening right now
- **Dead**: The process is not happening (either never started, or already ended)

This is useful for:
- Knowing if an animation/effect is currently playing
- Tying async operations to Unity object lifecycles
- Checking if a system is currently running
- Coordinating multiple concurrent processes
- Waiting for a process to end

**Key insight**: A dead lifetime is still useful! It tells you the process is NOT happening, which is valuable information.

## API Overview

### Creation
```csharp
// Create a new lifetime
var lifetime = new Lifetime();

// From MonoBehaviour (dies when object is destroyed)
var lifetime = this.Lifetime();

// From UniTask (dies when task completes)
Lifetime lifetime = someUniTask;
```

### State Checking
```csharp
// Check if alive (various ways, all null-safe)
bool alive = lifetime.IsAlive();  // false if null
bool alive = lifetime;            // Implicit conversion to bool

// Check if dead (null is considered dead)
bool dead = lifetime.IsDead();    // true if null
```

### Termination
```csharp
// Kill the lifetime (null-safe, no-op if null)
lifetime.Kill();

// Using statement (auto-kill when scope exits)
using (var lifetime = new Lifetime())
{
    // Operations...
} // Automatically killed here
```

### Composition
```csharp
// AND - lifetime dies when both complete
var combined = lifetime1 & someTask;

// OR - lifetime dies when either completes
var combined = lifetime1 | someTask;

// Parent-child relationship
var child = new Lifetime().SetParent(parentTask);
```

## Implicit Conversions

The power of `Lifetime` comes from its implicit conversions (all null-safe):

```csharp
// To UniTask - completes when lifetime is killed (or immediately if null)
UniTask task = lifetime;

// To CancellationToken - returns CancellationToken.None if null
CancellationToken token = lifetime;

// To bool - true if alive, false if dead or null
if (lifetime) { /* still alive */ }

// From UniTask - creates lifetime that dies with task
Lifetime lifetime = UniTask.Delay(1000);
```

## Usage Patterns

### 1. Object Lifecycle Management

Tie async operations to MonoBehaviour lifecycle:

```csharp
public class MyComponent : MonoBehaviour
{
    private async void Start()
    {
        // Operation automatically cancelled when object is destroyed
        await DoSomethingAsync(this.Lifetime());
    }

    private async UniTask DoSomethingAsync(Lifetime life)
    {
        while (life.IsAlive())
        {
            // Work continues until component is destroyed
            await UniTask.Delay(100);
        }
    }
}
```

### 2. Event Listener Cleanup

Automatically unsubscribe when lifetime ends:

```csharp
// Signal with automatic cleanup
signal.AddListener(OnEventFired, this.Lifetime());

// Unity event with automatic cleanup
unityEvent.AddListener(OnEventFired, this.Lifetime());
```

### 3. Animation/Effect Duration Control

Control effect duration and allow early termination:

```csharp
public class EffectController : MonoBehaviour
{
    private Lifetime effectLifetime;

    public async UniTask PlayEffect()
    {
        using (effectLifetime = new Lifetime())
        {
            // Effect plays until lifetime is killed
            await AnimateEffect(effectLifetime);
        }
    }

    public void StopEffect()
    {
        // Kill effect early
        effectLifetime.Kill();
    }

    private async UniTask AnimateEffect(Lifetime life)
    {
        // Animation runs until life is killed or naturally completes
        await UniTask.WhenAny(
            AnimationSequence(),
            life.AsUniTask()
        );
    }
}
```

### 4. Temporary Scope Management

Create temporary execution scopes with `using`:

```csharp
// From FtueManager.cs:822
using (WannaShowFtuePopup = new Lifetime())
{
    await ShowPopup();
    // Popup automatically closes when scope exits
}
```

### 5. Combining Multiple Conditions

Wait for multiple conditions using operators:

```csharp
// Wait for BOTH task completion AND lifetime end
var combinedLife = lifetime & longRunningTask;
await combinedLife;

// Wait for EITHER timeout OR manual cancellation
var timeoutLife = lifetime | UniTask.Delay(5000);
await timeoutLife;
```

### 6. Parent-Child Relationships

```csharp
// Child lifetime dies when parent task completes
var childLife = new Lifetime().SetParent(parentTask);

// Chaining
var life = new Lifetime()
    .SetParent(taskA)
    .SetParent(taskB);  // Dies when first parent completes
```

### 7. Awaitable Lifetime

Direct await support:

```csharp
// Wait until lifetime is killed
var lifetime = new Lifetime();
SomeOtherCode(() => lifetime.Kill());
await lifetime;  // Blocks until killed
```

## Real-World Examples

### Example 1: Dice Disappearing Animation (DiceController.cs)

```csharp
private Lifetime diceDisappearingLife;
private Lifetime disappearAwaiter;

public void AddDisappearAwaiter(Lifetime lifetime)
{
    // Combine multiple wait conditions
    disappearAwaiter &= lifetime;
}

public async UniTask Disappear(Lifetime freezeLife)
{
    // Wait for freeze to end
    await freezeLife;

    // Wait for any additional conditions
    await disappearAwaiter;

    // Cancellable delay
    await (diceDisappearingLife = delay.AsTask());
}

public void ForceDisappear()
{
    // Kill the delay early
    diceDisappearingLife.Kill();
}
```

### Example 2: Pawn Movement (PawnController.cs:85)

```csharp
public async UniTask Run(List<BoardTileView> path)
{
    // Animation strategy controls movement lifetime
    using Lifetime _ = animationStrategy.OnMoveStarted();

    // Movement logic...
    // Automatically cancelled when strategy decides
}
```

### Example 3: Camera Follow (BoardCameraView.cs)

```csharp
private Lifetime inertiaLifetime;

private void HandleDrag()
{
    // Stop previous inertia
    inertiaLifetime.Kill();

    // Start new inertia
    inertiaLifetime = new Lifetime();
    HandleInertia(inertiaLifetime).Forget();
}

private async UniTaskVoid HandleInertia(Lifetime lifetime)
{
    while (lifetime.IsAlive())
    {
        // Apply inertia until lifetime is killed
        await UniTask.Yield();
    }
}
```

## Benefits

1. **Semantic Clarity**: `effectLife.IsAlive()` clearly asks "is the effect happening?" vs `!token.IsCancellationRequested`
2. **Process-Oriented Thinking**: Represents processes, not cancellation mechanisms
3. **Queryable State**: Dead lifetimes remain useful - they tell you the process ended
4. **Memory Safety**: Prevents memory leaks by tying operations to object lifecycles
5. **Composability**: Combine multiple process lifetimes with logical operators
6. **Type Safety**: Compile-time checks via implicit conversions
7. **Less Boilerplate**: No manual CancellationTokenSource disposal needed
8. **Unity Integration**: Natural integration with MonoBehaviour lifecycle via `this.Lifetime()`
9. **Null Safety**: All operations handle null gracefully - null means "not happening"

## Limitations

1. **One-Way State Transition**: Once dead, stays dead forever (processes don't un-end themselves)
2. **No Cancellation Callbacks**: Unlike `CancellationToken.Register()`, no direct callback registration
3. **No Built-in Timeout**: Must combine with `UniTask.Delay()` for timeouts
4. **Unity-Specific**: Tightly coupled to UniTask, not portable to non-Unity contexts
5. **Single Process Per Instance**: One `Lifetime` = one process; can't reuse for multiple sequential processes

## Null Safety & Dead Lifetime Semantics

**Important**: `Lifetime` treats both `null` and killed lifetimes as **"process not happening"**. This is intentional and useful!

```csharp
Lifetime life = null;

// All of these are safe and express "process not happening":
life.Kill();           // No-op (process already not happening)
life.IsAlive();        // false (process not happening)
life.IsDead();         // true (process not happening)
if (life) { }          // False - process not happening
await life;            // Completes immediately (process not happening)
CancellationToken t = life;  // Returns CancellationToken.None
```

This design allows natural patterns:

```csharp
private Lifetime scrollLife;  // null = scroll not happening

public void StopScroll()
{
    scrollLife.Kill();  // Safe! Whether null or alive, afterward scroll is not happening
}

public void StartScroll()
{
    if (scrollLife.IsAlive()) return;  // Already scrolling, don't start again
    scrollLife = new Lifetime();       // New scroll process
    DoScroll(scrollLife).Forget();
}

public bool IsScrolling() => scrollLife.IsAlive();  // Works even if null!
```

**Key insight**: Dead/null lifetimes aren't "errors" - they're valid state representing "process not happening".

## Common Pitfalls

### ❌ Don't: Assume null lifetime represents an active process
```csharp
private Lifetime effectLife;  // null

public async UniTask WaitForEffect()
{
    // This completes immediately - null means "not happening"!
    await effectLife;
}
```

### ✅ Do: Initialize when starting a new process
```csharp
private Lifetime effectLife;

public async UniTask StartEffect()
{
    effectLife = new Lifetime();  // Process starts
    await PlayEffect(effectLife);
}

public bool IsEffectPlaying() => effectLife.IsAlive();
```

### ❌ Don't: Try to restart a process with the same instance
```csharp
private Lifetime processLife = new Lifetime();

public void RestartProcess()
{
    processLife.Kill();
    // processLife is still the old dead process!
    StartProcess(processLife);  // Won't work as expected
}
```

### ✅ Do: Create new lifetime for each process instance
```csharp
private Lifetime processLife;

public void RestartProcess()
{
    processLife?.Kill();  // End old process if running
    processLife = new Lifetime();  // Start new process
    StartProcess(processLife);
}
```

### ❌ Don't: Forget to end processes when not using `using`
```csharp
public void StartEffect()
{
    var life = new Lifetime();
    PlayEffect(life).Forget();
    // Process never ends - potential resource leak!
}
```

### ✅ Do: Use `using` or ensure manual cleanup
```csharp
public async UniTask StartEffect()
{
    using var life = new Lifetime();
    await PlayEffect(life);
    // Process automatically ends on exit
}
```

## Best Practices

1. **Think in processes**: One `Lifetime` = one process instance
2. **Use `using` when possible**: Ensures process ends when scope exits
3. **Tie to object lifecycle**: Use `this.Lifetime()` to make processes end with objects
4. **Leverage null safety**: null and dead lifetimes both mean "not happening"
5. **Name clearly**: Use descriptive names like `effectLife`, `animationLife`, `scrollLife`
6. **Create new instances**: Each process restart needs a new `Lifetime` instance
7. **Check before starting**: Use `if (!life.IsAlive())` to avoid overlapping processes
8. **Keep dead references**: Dead lifetimes are useful for querying "is it still happening?"

## Comparison with Alternatives

### vs. CancellationToken
- **Lifetime**: Represents processes ("is it happening?"), semantic, composable, awaitable
- **CancellationToken**: Represents cancellation signals, verbose, standard .NET pattern

### vs. IDisposable
- **Lifetime**: Process lifecycle, async-aware, state-queryable after disposal
- **IDisposable**: Resource cleanup, sync-only, fire-and-forget pattern

### vs. GameObject.activeInHierarchy
- **Lifetime**: Process state for any operation, works beyond GameObjects
- **activeInHierarchy**: Object state only, polling required, no async integration

### vs. bool isRunning
- **Lifetime**: Awaitable, composable, auto-cleanup, integrates with UniTask
- **bool**: Simple flag, manual management, no async features, error-prone

## Integration Points

The `Lifetime` integrates with:
- **UniTask**: Core async framework (implicit conversions)
- **MonoBehaviour**: Via `this.Lifetime()` extension (GameObjectEx.cs:29)
- **AsyncSignal**: Automatic listener cleanup (AsyncSignal.cs:24)
- **UnityEvent**: Automatic listener cleanup (UnityEventEx.cs:21)
- **CancellationToken**: Full compatibility via implicit conversion

## File Location

**Source**: `Assets/CMBA/Scripts/Tools/Lifetime.cs`

**Extensions**:
- `Assets/CMBA/Scripts/Tools/Extensions/GameObjectEx.cs:29` - MonoBehaviour integration
- `Assets/CMBA/Scripts/Tools/Extensions/AsyncSignalEx.cs` - Signal integration
- `Assets/CMBA/Scripts/Tools/Extensions/UnityEventEx.cs:21` - UnityEvent integration

## Summary

`Lifetime` is a semantic representation of process state that answers "is this happening right now?". Unlike cancellation tokens which focus on stopping operations, `Lifetime` focuses on representing whether processes are active. Dead and null lifetimes are equally useful - they both mean "not happening". This process-oriented thinking makes code more intuitive and safer, especially in Unity/UniTask environments where you need to track animations, effects, user interactions, and tie them to object lifecycles. The key is understanding that `Lifetime` represents processes, not cancellation mechanisms.
