namespace Yogurt.Arena;

public static class Wait
{ 
    public static async UniTask While(Func<bool> predicate, Life life = default)
    {
        Life token = life
            ? life
            : Game.Life;
        
        while (token && predicate())
        {
            await UniTask.NextFrame(Game.Token);
        }

        if (!token)
            await UniTask.FromCanceled();
    }

    public static async UniTask While<T>(Func<T, bool> predicate, T state, Life life = default)
    {
        Life token = life
            ? life
            : Game.Life;
        
        while (token && predicate(state))
        {
            await UniTask.NextFrame(Game.Token);
        }
        
        if (!token)
            await UniTask.FromCanceled();
    }

    public static UniTask Until(Func<bool> predicate, Life life = default)
    {
        return While(p => !p(), predicate, life);
    }

    public static UniTask Update()
    {
        return UniTask.NextFrame(Game.Token);
    }

    public static UniTask Seconds(float seconds, Life life = default)
    {
        float startTime = UnityEngine.Time.time;
        return While(ShouldWait, (startTime, seconds), life);

        static bool ShouldWait((float startTime, float seconds) tuple) 
            => UnityEngine.Time.time - tuple.startTime < tuple.seconds;
    }
}