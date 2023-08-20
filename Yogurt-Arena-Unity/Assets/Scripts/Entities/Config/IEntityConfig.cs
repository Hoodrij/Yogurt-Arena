using System.Collections.Generic;

namespace Yogurt.Arena
{
    public interface IEntityConfig
    {
        IEnumerable<IComponent> GetComponents();
    }
}