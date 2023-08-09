using System.Linq;

namespace Yogurt.Arena
{
    public struct GetItemFactoryJob
    {
        public IItemFactoryJob Run(ItemType type)
        {
            ItemConfigAspect config = Query.Of<ItemConfigAspect>()
                .FirstOrDefault(item => item.Config.Type == type);
                
            return config.Config.FactoryJob;
        }
    }
}