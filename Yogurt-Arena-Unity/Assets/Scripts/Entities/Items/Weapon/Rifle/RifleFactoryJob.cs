using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class RifleFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RifleData data = Query.Single<Data>().Rifle;

            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseRifleJob(), data);
            item.Item.Job.Run(item, owner.Entity);
            
            return item;
        }
    }
}