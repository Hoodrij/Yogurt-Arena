namespace Yogurt.Arena;

public static class Wait
{ 
    public static async UniTask While(Func<bool> predicate, Lifetime life = default)
    {
        Lifetime token = life
            ? life
            : Game.Life;
        
        while (token && predicate())
        {
            await UniTask.NextFrame(Game.Token);
        }

        if (!token)
            await UniTask.FromCanceled();
    }

    public static async UniTask While<T>(Func<T, bool> predicate, T state, Lifetime life = default)
    {
        Lifetime token = life
            ? life
            : Game.Life;
        
        while (token && predicate(state))
        {
            await UniTask.NextFrame(Game.Token);
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
        return UniTask.NextFrame(Game.Token);
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
            .AttachExternalCancellation(Game.Token);
    }
        
    public static UniTask All(params UniTask[] tasks)
    {
        return UniTask.WhenAll(tasks)
            .AttachExternalCancellation(Game.Token);
    }
}