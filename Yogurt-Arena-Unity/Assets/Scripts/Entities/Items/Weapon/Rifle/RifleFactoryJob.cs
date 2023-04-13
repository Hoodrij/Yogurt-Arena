using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class RifleFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            ItemAspect itemAspect = Entity.Create()
                .Add(Query.Single<Data>().Rifle)
                .Add(new Item
                {
                    Job = new UseRifleJob()
                })
                .As<ItemAspect>();

            itemAspect.Entity.SetParent(owner.Entity);
            itemAspect.Item.Job.Run(itemAspect, owner.Entity);
            
            return itemAspect;
        }
    }
}