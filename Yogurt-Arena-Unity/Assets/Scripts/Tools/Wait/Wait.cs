using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class Wait
    {
        private static Lifetime AppLifetime => Application.exitCancellationToken;
        
        public static UniTask While(Func<bool> predicate, Lifetime life = null)
        {
            if (!predicate.Invoke()) 
                return UniTask.CompletedTask;

            life ??= AppLifetime;
            return UniTask.WaitWhile(predicate, cancellationToken: life);
        }

        public static UniTask Until(Func<bool> predicate, Lifetime life = null)
        {
            return While(Predicate, life);

            bool Predicate() => !predicate();
        }

        public static UniTask Update()
        {
            return UniTask.NextFrame(cancellationToken: AppLifetime);
        }

        public static UniTask Seconds(float seconds, Lifetime life = null)
        {
            float startTime = UnityEngine.Time.time;
            life ??= AppLifetime;
            
            return seconds > 0 
                ? Until(IsCompleted, life) 
                : UniTask.CompletedTask;

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
}