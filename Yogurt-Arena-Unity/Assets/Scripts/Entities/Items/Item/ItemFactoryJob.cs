using UnityEngine;

namespace Yogurt.Arena
{
    public interface IItemFactoryJob
    {
        Awaitable<ItemAspect> Run(AgentAspect owner);
    }
    
    public struct ItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner, IItemUseJob job, EItemType type)
        {
            ItemAspect itemAspect = World.Create()
                .Add(new Item
                {
                    Type = type,
                    Job = job,
                })
                .Add(new OwnerState
                {
                    Owner = owner
                })
                .As<ItemAspect>();
            
            itemAspect.Entity.SetParent(owner.Entity);

            return itemAspect;
        }
    }
}