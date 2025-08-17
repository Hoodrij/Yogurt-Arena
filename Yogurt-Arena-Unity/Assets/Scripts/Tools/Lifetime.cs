using System.Diagnostics;
using System.Threading;

namespace Yogurt.Arena;

[DebuggerDisplay("{Name}")]
public class Lifetime : IDisposable
{
    internal readonly CancellationTokenSource Cts = new();

    private string Name => $"IsAlive - {this.IsAlive()}";

    void IDisposable.Dispose() => this.Kill();

    public static implicit operator UniTask(Lifetime life) => life.AsUniTask();
    public static implicit operator Lifetime(UniTask task) => new Lifetime().SetParent(task);
    public static implicit operator CancellationToken(Lifetime life) => life?.Cts?.Token ?? CancellationToken.None;
    public static implicit operator Lifetime(CancellationToken token) => token.AsLife();
    public static implicit operator bool(Lifetime life) => life.IsAlive();
    public static Lifetime operator &(Lifetime a, UniTask b) => a.And(b);
    public static Lifetime operator |(Lifetime a, UniTask b) => a.Or(b);
}

public static class LifetimeEx
{
    public static void Kill(this Lifetime life)
    {
        life?.Cts?.Cancel();
    }

    public static bool IsAlive(this Lifetime lifetime)
    {
        if (lifetime == null)
            return false;

        return !lifetime.Cts.IsCancellationRequested;
    }

    public static bool IsDead(this Lifetime lifetime)
    {
        return !lifetime;
    }

    public static UniTask AsUniTask(this Lifetime life)
    {
        if (life.IsDead())
            return UniTask.CompletedTask;

        return life.Cts.Token.ToUniTask().Item1;
    }
        
    public static Lifetime AsLife(this CancellationToken token)
    {
        if (token.IsCancellationRequested)
            return UniTask.CompletedTask;
            
        Lifetime life = new();
        token.Register(() => life.Kill());
        return life;
    }

    public static Lifetime And(this Lifetime a, UniTask b)
    {
        Lifetime resultLife = new();
        KillWhenAll();
        return resultLife;

        async void KillWhenAll()
        {
            await UniTask.WhenAll(a, b);
            resultLife.Kill();
        }
    }

    public static Lifetime Or(this Lifetime a, UniTask b)
    {
        Lifetime resultLife = new();
        KillWhenAny();
        return resultLife;

        async void KillWhenAny()
        {
            await UniTask.WhenAny(a, b);
            resultLife.Kill();
        }
    }

    public static Lifetime SetParent(this Lifetime one, UniTask parent)
    {
        KillWithParent();
        return one;

        async void KillWithParent()
        {
            await parent;
            one.Kill();
        }
    }

    public static UniTask.Awaiter GetAwaiter(this Lifetime life)
    {
        return ((UniTask)life).GetAwaiter();
    }
}