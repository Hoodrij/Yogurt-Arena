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
            return UniTask.WaitWhile(predicate, cancellationToken: lifetime);
        }
        
        public static UniTask Until(Func<bool> predicate)
        {
            return UniTask.WaitUntil(predicate, cancellationToken: lifetime);
        }

        public static UniTask Update()
        {
            return UniTask.NextFrame(cancellationToken: lifetime);
        }

        public static UniTask Seconds(float seconds)
        {
            return UniTask.WaitForSeconds(seconds, cancellationToken: lifetime);
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