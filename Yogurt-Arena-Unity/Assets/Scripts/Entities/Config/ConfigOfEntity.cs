using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class ConfigOfEntity : IComponent
    {
        public IEnumerable<IComponent> Components;
    }
}