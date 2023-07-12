using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public interface IAsset<TComponent> where TComponent : Component
    {
        public Awaitable<TComponent> Spawn();
    }
}