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
                    CancellationTokenSource cts = new CancellationTokenSource();
                    CancellationToken token = cts.Token;
                    
                    return UniTask.WaitWhile(() =>
                    {
                        if (entity.Exist)
                            return predicate();

                        cts.Cancel();
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

        public static UniTask Update()
        {
            return UniTask.NextFrame(cancellationToken: AppLifetime);
        }

        public static UniTask Seconds(float seconds, Entity entity = default)
        {
            float startTime = UnityEngine.Time.time;
            if (seconds > 0)
            {
                return Until(IsCompleted, entity);
            }
            return UniTask.CompletedTask;


            bool IsCompleted()
            {
                float now = UnityEngine.Time.time;
                return now - startTime >= seconds;
            }
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
}