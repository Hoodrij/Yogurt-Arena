using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class Wait
    {
        private static CancellationToken AppLifetime => Application.exitCancellationToken;
        
        public static UniTask While(Func<bool> predicate, Entity entity = default)
        {
            if (predicate.Invoke())
            {
                if (entity.Exist)
                {
                    CancellationToken token = entity.Lifetime();
                    return UniTask.WaitWhile(() =>
                    {
                        if (entity.Exist)
                            return predicate();
                        
                        return true;
                    }, cancellationToken: token);
                }
                else
                {
                    return UniTask.WaitWhile(predicate, cancellationToken: AppLifetime);
                }
            }
            return UniTask.CompletedTask;
        }

        public static UniTask Until(Func<bool> predicate, Entity entity = default)
        {
            return While(() => !predicate(), entity);
        }

        public static UniTask Update(Entity entity = default)
        {
            CancellationToken token = entity.Exist ? entity.Lifetime() : AppLifetime;
            return UniTask.NextFrame(cancellationToken: token);
        }

        public static UniTask Seconds(float seconds, Entity entity = default)
        {
            if (seconds > 0)
            {
                CancellationToken token = entity.Exist ? entity.Lifetime() : AppLifetime;
                return UniTask.WaitForSeconds(seconds, cancellationToken: token);
            }
            return UniTask.CompletedTask;
        }

        public static UniTask Any(params UniTask[] tasks)
        {
            return UniTask.WhenAny(tasks)
                .AttachExternalCancellation(Application.exitCancellationToken);
        }
        
        public static UniTask All(params UniTask[] tasks)
        {
            return UniTask.WhenAll(tasks)
                .AttachExternalCancellation(Application.exitCancellationToken);
        }
    }
}