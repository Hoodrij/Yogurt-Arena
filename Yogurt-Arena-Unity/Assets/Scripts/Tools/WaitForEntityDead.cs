using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForEntityDead
    {
        public async UniTask Run(Entity entity)
        {
            await Wait.While(() => entity.Exist);
        }
    }
}