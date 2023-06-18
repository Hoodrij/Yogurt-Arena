using System;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public static class UniTaskEx
    {
        public static UniTask Yield()
        {
            return UniTask.NextFrame().AttachLifetime();
        }
        
        public static async UniTask<T> WhenAny<T>(UniTask<T> task1, UniTask<T> task2)
        {
            (int winArgumentIndex, T result1, T result2) = await UniTask.WhenAny(task1, task2);
            return winArgumentIndex == 0 ? result1 : result2;
        }
        
        public static async UniTask<T> WhenAny<T>(Func<UniTask<T>> task1, Func<UniTask<T>> task2)
        {
            (int winArgumentIndex, T result1, T result2) = await UniTask.WhenAny(task1(), task2());
            return winArgumentIndex == 0 ? result1 : result2;
        }

        public static UniTask AttachLifetime(this UniTask task)
        {
            return task.AttachExternalCancellation(Query.Single<EthernalLifetime>());
        }
        
        public static UniTask<T> AttachLifetime<T>(this UniTask<T> task)
        {
            return task.AttachExternalCancellation(Query.Single<EthernalLifetime>());
        }
    }
}