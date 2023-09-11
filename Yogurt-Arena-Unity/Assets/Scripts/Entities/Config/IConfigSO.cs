using System.Collections.Generic;

namespace Yogurt.Arena
{
    public interface IConfigSO
    {
        IEnumerable<IComponent> GetComponents();
    }
}