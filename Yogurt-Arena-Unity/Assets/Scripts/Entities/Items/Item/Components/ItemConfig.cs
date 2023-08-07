using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class ItemConfig : IComponent
    {
        public EItemType Type;
        public EItemTags Tags;
        public IItemFactoryJob FactoryJob;
        public IItemUseJob UseJob;
    }
}