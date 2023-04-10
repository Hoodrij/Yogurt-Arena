using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class ItemsCollection : IComponent
    {
        public List<ItemAspect> Value = new List<ItemAspect>();

        public void Add(ItemAspect item) => Value.Add(item);
    }
}