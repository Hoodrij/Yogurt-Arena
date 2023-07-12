using UnityEngine;

namespace Yogurt.Arena.Tools
{
    public interface IAsset<TComponent> where TComponent : Component
    {
        public Awaitable<TComponent> Spawn();
    }
}