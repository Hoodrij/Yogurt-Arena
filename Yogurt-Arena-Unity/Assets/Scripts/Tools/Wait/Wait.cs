using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class Wait
    {
        static CancellationToken lifetime => Application.exitCancellationToken;
        
        public static UniTask While(Func<bool> predicate)
        {
            if (predicate.Invoke())
            {
                return UniTask.WaitWhile(predicate, cancellationToken: lifetime);
            }
            return UniTask.CompletedTask;
        }
        
        public static UniTask Until(Func<bool> predicate)
        {
            if (!predicate.Invoke())
            {
                return UniTask.WaitUntil(predicate, cancellationToken: lifetime);
            }
            return UniTask.CompletedTask;
        }

        public static UniTask Update()
        {
            return UniTask.NextFrame(cancellationToken: lifetime);
        }

        public static UniTask Seconds(float seconds)
        {
            if (seconds > 0)
            {
                return UniTask.WaitForSeconds(seconds, cancellationToken: lifetime);
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