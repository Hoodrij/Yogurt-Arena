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
    }
}