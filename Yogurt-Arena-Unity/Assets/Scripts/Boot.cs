namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            Destroy(gameObject);
            PrimeTweenConfig.warnBenchmarkWithAsserts = false;
            PrimeTweenConfig.warnEndValueEqualsCurrent = false;
            PrimeTweenConfig.warnZeroDuration = false;
            PrimeTweenConfig.warnTweenOnDisabledTarget = false;
            PrimeTweenConfig.warnStructBoxingAllocationInCoroutine = false;
            
            await new GameFactoryJob().Run();
            new RunGameLoopJob().Run();
        }
    }
}