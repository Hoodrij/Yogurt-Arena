using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct HealingPotionFactoryJob : IItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            HealingPotionData data = Query.Single<Data>().HealingPotion;

            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseHealingPotionJob(), 
                EItemType.HealingPotion);
            item.Add(data);

            await item.Item.Job.Run(item);
            
            return item;
        }
    }
}