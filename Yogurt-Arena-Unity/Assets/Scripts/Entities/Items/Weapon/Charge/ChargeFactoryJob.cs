using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            ChargeData data = Query.Single<Data>().Charge;
            
            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseChargeJob(), data);
            item.Item.Job.Run(item, owner);
            
            return item;
        }
    }
}