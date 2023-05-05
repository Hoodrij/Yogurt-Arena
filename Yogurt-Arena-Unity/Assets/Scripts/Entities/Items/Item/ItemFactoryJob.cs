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
                .Add(new OwnerState
                {
                    Owner = owner
                })
                .As<ItemAspect>();
            
            itemAspect.Entity.SetParent(owner.Entity);
            owner.Items.Add(itemAspect);

            return itemAspect;
        }
    }
}