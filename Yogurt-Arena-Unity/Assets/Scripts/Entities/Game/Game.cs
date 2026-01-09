using System.Threading;

namespace Yogurt.Arena;

public record struct Game : IComponent
{
    public static CancellationToken Token => Application.exitCancellationToken;
    public static Life Life { get; private set; }

    public Game()
    {
        Life = Application.exitCancellationToken;
        
        PrimeTweenConfig.warnBenchmarkWithAsserts = false;
        PrimeTweenConfig.warnEndValueEqualsCurrent = false;
        PrimeTweenConfig.warnZeroDuration = false;
        PrimeTweenConfig.warnTweenOnDisabledTarget = false;
        PrimeTweenConfig.warnStructBoxingAllocationInCoroutine = false;
    }
}