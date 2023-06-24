using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public static class Wait
    {
        private static CancellationToken Lifetime => Query.Single<EthernalLifetime>();
        
        public static UniTask Until(Func<bool> predicate)
        {
            return UniTask.WaitUntil(predicate, cancellationToken: Lifetime);
        }

        public static UniTask While(Func<bool> predicate)
        {
            return UniTask.WaitWhile(predicate, cancellationToken: Lifetime);
        }

        public static UniTask Update()
        {
            return UniTask.NextFrame(cancellationToken: Lifetime);
        }

        public static UniTask Seconds(float seconds)
        {
            return UniTask.Delay(seconds.ToSeconds(), cancellationToken: Lifetime);
        }

        public static UniTask Any(params UniTask[] array)
        {
            return UniTask.WhenAny(array)
                .AttachExternalCancellation(Lifetime);
        }
        
        public static UniTask All(params UniTask[] array)
        {
            return UniTask.WhenAll(array)
                .AttachExternalCancellation(Lifetime);
        }
    }
}