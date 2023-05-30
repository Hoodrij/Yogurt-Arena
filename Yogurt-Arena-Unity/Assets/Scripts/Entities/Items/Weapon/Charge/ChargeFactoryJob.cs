using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            WeaponData data = Query.Single<Data>().Charge;
            
            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseChargeJob(), EItemType.Charge);
            item.Add(data);
            
            item.Item.Job.Run(item);
            
            return item;
        }
    }
}