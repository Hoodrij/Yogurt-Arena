using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemFactoryJob
    {
        public async UniTask<ItemAspect> Run<T>(AgentAspect owner, IItemUseJob job, T data) where T : IComponent
        {
            ItemAspect itemAspect = World.Create()
                .Add(data)
                .Add(new Item
                {
                    Job = job
                })
                .As<ItemAspect>();

            itemAspect.Entity.SetParent(owner.Entity);
            itemAspect.Item.Job.Run(itemAspect, owner.Entity);
            
            return itemAspect;
        }
    }
}