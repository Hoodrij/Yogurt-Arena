using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForEntityDead
    {
        public async UniTask Run(Entity entity)
        {
            await UniTask.WaitWhile(() => entity.Exist);
        }
    }
}