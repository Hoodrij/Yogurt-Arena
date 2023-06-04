using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class Inventory : IComponent
    {
        public ItemAspect Weapon;
        public List<ItemAspect> Items = new List<ItemAspect>();
    }
}