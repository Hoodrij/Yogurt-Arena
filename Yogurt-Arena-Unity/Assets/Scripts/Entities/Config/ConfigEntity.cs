using System.Collections.Generic;

namespace Yogurt.Arena
{
    public class ConfigEntity : IComponent
    {
        public IEnumerable<IComponent> Components;
    }
}