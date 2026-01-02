namespace Yogurt.Arena;

public static class Wait
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnterPlayMode() => AppLifetime = Application.exitCancellationToken;
    private static Lifetime AppLifetime { get; set; } = Application.exitCancellationToken;
        
    public static async UniTask While(Func<bool> predicate, Lifetime life = default)
    {
        Lifetime token = life
            ? life | AppLifetime
            : AppLifetime;
        
        while (token && predicate())
        {
            await UniTask.NextFrame();
        }

        if (!token)
            await UniTask.FromCanceled();
    }

    public static async UniTask While<T>(Func<T, bool> predicate, T state, Lifetime life = default)
    {
        Lifetime token = life 
            ? life | AppLifetime
            : AppLifetime;
        
        while (token && predicate(state))
        {
            await UniTask.NextFrame();
        }
        
        if (!token)
            await UniTask.FromCanceled();
    }

    public static UniTask Until(Func<bool> predicate, Lifetime life = default)
    {
        return While(p => !p(), predicate, life);
    }

    public static UniTask Update()
    {
        return UniTask.NextFrame(AppLifetime);
    }

    public static UniTask Seconds(float seconds, Lifetime life = default)
    {
        float startTime = UnityEngine.Time.time;
        return Until(IsCompleted, life);

        bool IsCompleted() => UnityEngine.Time.time - startTime >= seconds;
    }

    public static UniTask Any(params UniTask[] tasks)
    {
        return UniTask.WhenAny(tasks)
            .AttachExternalCancellation(AppLifetime);
    }
        
    public static UniTask All(params UniTask[] tasks)
    {
        return UniTask.WhenAll(tasks)
            .AttachExternalCancellation(AppLifetime);
    }
}