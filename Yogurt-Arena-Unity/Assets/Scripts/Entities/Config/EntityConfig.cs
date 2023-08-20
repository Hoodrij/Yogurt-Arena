using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class EntityConfig : IComponent
    {
        public IEnumerable<IComponent> Components;

        public void AppendTo(Entity entity)
        {
            foreach (IComponent component in Components)
            {
                entity.Add(component);
            }
        }
    }
}