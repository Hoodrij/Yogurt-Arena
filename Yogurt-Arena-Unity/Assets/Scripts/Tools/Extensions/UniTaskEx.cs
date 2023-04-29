using System;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public static class UniTaskEx
    {
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

        public static bool TryGetResult<T>(this UniTask<T> task, out T result)
        {
            if (task.Status == UniTaskStatus.Succeeded)
            {
                result = task.GetAwaiter().GetResult();
                return true;
            }

            result = default;
            return false;
        }
    }
}