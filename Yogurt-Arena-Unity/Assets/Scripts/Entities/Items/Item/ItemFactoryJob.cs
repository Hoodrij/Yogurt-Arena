using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IItemFactoryJob
    {
        UniTask<ItemAspect> Run(AgentAspect owner);
    }
    
    public struct ItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner, IItemUseJob job, EItemType type)
        {
            ItemAspect itemAspect = World.Create()
                .Add(new Item
                {
                    Job = job,
                    Type = type
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