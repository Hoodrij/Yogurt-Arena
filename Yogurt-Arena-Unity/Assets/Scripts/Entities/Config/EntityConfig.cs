using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class EntityConfig : IComponent
    {
        public IEnumerable<IComponent> Components;
    }
}