using System;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public static class Wait
    {
        public static UniTask While(Func<bool> predicate)
        {
            return UniTask.WaitWhile(predicate);
        }
        
        public static UniTask Until(Func<bool> predicate)
        {
            return UniTask.WaitUntil(predicate);
        }

        public static UniTask Update()
        {
            return UniTask.NextFrame();
        }

        public static UniTask Seconds(float seconds)
        {
            return UniTask.WaitForSeconds(seconds);
        }

        public static UniTask Any(params UniTask[] tasks)
        {
            return UniTask.WhenAny(tasks);
        }
        
        public static UniTask All(params UniTask[] tasks)
        {
            return UniTask.WhenAll(tasks);
        }
    }
}