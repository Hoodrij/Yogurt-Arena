using System.Threading;
using UnityEngine.Pool;

namespace Yogurt.Arena;

public struct RifleBulletBehaviorJob
{
    public async UniTask Run(BulletAspect bullet)
    {
        CollisionInfo collision = default;

        UniTask collisionTask = DetectHit();
        new RifleMoveBulletJob().Run(bullet);
        
        await collisionTask.Or(WaitForLifeTime());
            
        if (collision.IsValid)
        {
            new DealDamageJob().Run(collision.Entity, bullet.Config.Damage);
            bullet.Body.Position = bullet.View.transform.position = collision.Position;
        }
            
        await new KillBulletJob().Run(bullet);
        return;


        async UniTask DetectHit()
        {
            collision = await new WaitForBulletHitJob().Run(bullet);
        }
        async UniTask WaitForLifeTime()
        {
            await new WaitForBulletLiteTimeJob().Run(bullet);
        }
    }
}

public static class uniex
{
    public static async UniTask Or(this UniTask a, UniTask b)
    {
        OrCompletionSource src = OrCompletionSource.Create();
        a.GetAwaiter().SourceOnCompleted(s => ((OrCompletionSource)s).OnTaskCompleted(), src);
        b.GetAwaiter().SourceOnCompleted(s => ((OrCompletionSource)s).OnTaskCompleted(), src);
        await src.Task;
    }

    public static async UniTask And(this UniTask a, UniTask b)
    {
        AndCompletionSource src = AndCompletionSource.Create();
        a.GetAwaiter().SourceOnCompleted(s => ((AndCompletionSource)s).OnTaskCompleted(), src);
        b.GetAwaiter().SourceOnCompleted(s => ((AndCompletionSource)s).OnTaskCompleted(), src);
        await src.Task;
    }

    private class OrCompletionSource : IUniTaskSource
    {
        private static readonly ObjectPool<OrCompletionSource> pool = new(() => new OrCompletionSource());
        
        private UniTaskCompletionSourceCore<AsyncUnit> core;
        private int remaining;

        public UniTask Task => new UniTask(this, core.Version);

        public static OrCompletionSource Create()
        {
            OrCompletionSource src = pool.Get();
            src.core.Reset();
            src.remaining = 2;
            return src;
        }

        public void OnTaskCompleted()
        {
            if (Interlocked.Decrement(ref remaining) == 1)
            {
                core.TrySetResult(AsyncUnit.Default);
            }
            else if (remaining == 0)
            {
                pool.Release(this);
            }
        }

        public UniTaskStatus GetStatus(short token) => core.GetStatus(token);
        public UniTaskStatus UnsafeGetStatus() => core.UnsafeGetStatus();
        public void GetResult(short token) => core.GetResult(token);
        public void OnCompleted(Action<object> continuation, object state, short token) => core.OnCompleted(continuation, state, token);
    }

    private class AndCompletionSource : IUniTaskSource
    {
        private static readonly ObjectPool<AndCompletionSource> pool = new(() => new AndCompletionSource());

        private UniTaskCompletionSourceCore<AsyncUnit> core;
        private int remaining;

        public UniTask Task => new UniTask(this, core.Version);

        public static AndCompletionSource Create()
        {
            AndCompletionSource src = pool.Get();
            src.core.Reset();
            src.remaining = 2;
            return src;
        }

        public void OnTaskCompleted()
        {
            if (Interlocked.Decrement(ref remaining) == 0)
            {
                core.TrySetResult(AsyncUnit.Default);
                pool.Release(this);
            }
        }

        public UniTaskStatus GetStatus(short token) => core.GetStatus(token);
        public UniTaskStatus UnsafeGetStatus() => core.UnsafeGetStatus();
        public void GetResult(short token) => core.GetResult(token);
        public void OnCompleted(Action<object> continuation, object state, short token) => core.OnCompleted(continuation, state, token);
    }
}