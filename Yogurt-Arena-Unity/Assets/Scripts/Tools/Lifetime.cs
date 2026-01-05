using System.Diagnostics;
using System.Threading;
using UnityEngine.Pool;

namespace Yogurt.Arena;

[DebuggerDisplay("{Name}")]
public readonly record struct Lifetime() : IDisposable
{
    internal readonly int Id = LifetimePool.Pop();

    private string Name => $"IsAlive - {this.IsAlive()}";

    void IDisposable.Dispose() => this.Kill();
    public override string ToString() => Name;

    public static implicit operator UniTask(Lifetime life) => life.AsUniTask();
    public static implicit operator Lifetime(UniTask task) => new Lifetime().SetParent(task);
    public static implicit operator CancellationToken(Lifetime life) => LifetimePool.GetToken(life);
    public static implicit operator Lifetime(CancellationToken token) => token.AsLife();
    public static implicit operator bool(Lifetime life) => life.IsAlive();
    public static Lifetime operator &(Lifetime a, Lifetime b) => a.And(b);
    public static Lifetime operator |(Lifetime a, Lifetime b) => a.Or(b);
}

public static class LifetimeAPI
{
    public static void Kill(this Lifetime life)
        => LifetimePool.Kill(life);

    public static bool IsAlive(this Lifetime life)
        => LifetimePool.IsAlive(life);

    public static bool IsDead(this Lifetime life) 
        => !life;

    public static UniTask AsUniTask(this Lifetime life)
        => LifetimePool.AsUniTask(life);
    
    public static Lifetime AsLife(this CancellationToken token) 
        => new Lifetime().SetParent(token.WaitUntilCanceled().AsTask());

    public static Lifetime And(this Lifetime a, Lifetime b) 
        => new Lifetime().SetParent(LifetimePool.All(a, b));

    public static Lifetime Or(this Lifetime a, Lifetime b)
        => new Lifetime().SetParent(LifetimePool.Any(a, b));

    public static Lifetime SetParent(this Lifetime life, UniTask parent)
    {
        KillWithParent(life, parent).Forget();
        return life;
        
        static async UniTask KillWithParent(Lifetime life, UniTask parent)
        {
            await parent;
            life.Kill();
        }
    }

    public static UniTask.Awaiter GetAwaiter(this Lifetime life)
        => LifetimePool.GetAwaiter(life);
}

internal static class LifetimePool
{
    private static readonly HashSet<int> activeLifetimes = new(capacity: 31);
    private static readonly Dictionary<int, CancellationTokenSource> tokenSources = new(capacity: 31);
    private static readonly Dictionary<int, List<AutoResetUniTaskCompletionSource>> completionSources = new(capacity: 31);
    private static int nextId = 1;
    
    public static int Pop()
    {
        int id = nextId++;
        activeLifetimes.Add(id);
        return id;
    }

    public static bool IsAlive(Lifetime lifetime)
    {
        return activeLifetimes.Contains(lifetime.Id);
    }

    public static async UniTask AsUniTask(Lifetime lifetime)
    {
        await lifetime;
    }

    public static void Kill(Lifetime lifetime)
    {
        int id = lifetime.Id;
        activeLifetimes.Remove(id);

        if (tokenSources.TryGetValue(id, out var cts))
        {
            cts.Cancel();
            cts.Dispose();
            tokenSources.Remove(id);
        }

        if (completionSources.Remove(id, out var sources))
        {
            foreach (var source in sources)
            {
                source.TrySetResult();
            }
            ListPool<AutoResetUniTaskCompletionSource>.Release(sources);
        }
    }

    public static CancellationToken GetToken(Lifetime lifetime)
    {
        if (lifetime.IsDead())
            return CancellationToken.None;
            
        if (tokenSources.TryGetValue(lifetime.Id, out var cts))
            return cts.Token;

        cts = new CancellationTokenSource();
        tokenSources[lifetime.Id] = cts;
        return cts.Token;
    }
    
    public static async UniTask AsTask(this CancellationTokenAwaitable awaitable)
    {
        await awaitable;
    }
    
    public static async UniTask All(Lifetime a, Lifetime b)
    {
        var srcA = AutoResetUniTaskCompletionSource.Create();
        var srcB = AutoResetUniTaskCompletionSource.Create();
        Run(a, srcA).Forget();
        Run(b, srcB).Forget();
        await srcA.Task;
        await srcB.Task;
    }

    public static async UniTask Any(Lifetime a, Lifetime b)
    {
        var src = AutoResetUniTaskCompletionSource.Create();
        Run(a, src).Forget();
        Run(b, src).Forget();
        await src.Task;
    }
    
    private static async UniTask Run(Lifetime task, AutoResetUniTaskCompletionSource source)
    {
        await task;
        source.TrySetResult();
    }

    public static UniTask.Awaiter GetAwaiter(Lifetime life)
    {
        if (life.IsDead())
            return UniTask.CompletedTask.GetAwaiter();

        var source = AutoResetUniTaskCompletionSource.Create();

        if (!completionSources.TryGetValue(life.Id, out var sources))
        {
            ListPool<AutoResetUniTaskCompletionSource>.Get(out sources);
            completionSources[life.Id] = sources;
        }

        sources.Add(source);
        return source.Task.GetAwaiter();
    }
}