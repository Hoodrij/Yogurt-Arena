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
        => life?.Cts?.Cancel();

    public static bool IsAlive(this Lifetime lifetime) 
        => lifetime != null && !lifetime.Cts.IsCancellationRequested;

    public static bool IsDead(this Lifetime lifetime) 
        => !lifetime;

    public static UniTask AsUniTask(this Lifetime life) 
        => life.IsDead() ? UniTask.CompletedTask : life.Cts.Token.ToUniTask().Item1;
    
    public static Lifetime AsLife(this CancellationToken token) 
        => new Lifetime().SetParent(token.ToUniTask().Item1);

    public static Lifetime And(this Lifetime a, UniTask b) 
        => new Lifetime()
            .SetParent(UniTask.WhenAll(a, b));

    public static Lifetime Or(this Lifetime a, UniTask b)
        => new Lifetime()
            .SetParent(UniTask.WhenAny(a, b));

    public static Lifetime SetParent(this Lifetime one, UniTask parent)
    {
        parent.ContinueWith(one.Kill);
        return one;
    }

    public static UniTask.Awaiter GetAwaiter(this Lifetime life) 
        => life.AsUniTask().GetAwaiter();
}