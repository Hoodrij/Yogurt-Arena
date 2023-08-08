using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IItemFactoryJob
    {
        UniTask<ItemAspect> Run(AgentAspect owner);
    }
    
    public struct ItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(IConfig config, AgentAspect owner)
        {
            Entity entity = World.Create()
                .Add(new OwnerState
                {
                    Owner = owner
                });
            
            config.AppendTo(entity);
            
            entity.SetParent(owner.Entity);

            return entity.As<ItemAspect>();
        }
    }
}