using System;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForEntityChanged
    {
        public async UniTask Run(Func<Entity> entityGetter)
        {
            Entity initialEntity = entityGetter.Invoke();

            await UniTask.WaitWhile(() => initialEntity == entityGetter.Invoke());
        }
    }
}