using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct HealingPotionFactoryJob : IItemFactoryJob
    {
        public async UniTask Run(ItemAspect item)
        {
            item.Config.UseJob.Run(item);
        }
    }
}