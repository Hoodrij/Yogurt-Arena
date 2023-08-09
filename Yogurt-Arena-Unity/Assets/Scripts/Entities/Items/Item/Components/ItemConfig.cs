using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class ItemConfig : IComponent
    {
        public ItemType Type;
        public ItemTags Tags;
        public IItemFactoryJob FactoryJob;
        public IItemUseJob UseJob;
    }
}