using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct HealingPotionFactoryJob : IItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            HealingPotionConfig config = Query.Single<HealingPotionConfig>();

            ItemAspect item = await new ItemFactoryJob().Run(config, owner); 

            item.Config.UseJob.Run(item);
            
            return item;
        }
    }
}