using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public class WorldConfig : ScriptableObject, IComponent, IEntityConfig
    {
        public Asset<World> World;

        public IEnumerable<IComponent> GetComponents()
        {
            yield return this;
        }
    }
}