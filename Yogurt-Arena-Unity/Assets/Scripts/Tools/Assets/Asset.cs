using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yogurt.Roguelike.Tools
{
    [Serializable]
    public class Asset<TComponent> : IAsset<TComponent> where TComponent : Component
    {
        public GameObject Prefab;

        public async Awaitable<TComponent> Spawn()
        {
            TComponent result = Object.Instantiate(Prefab).GetComponent<TComponent>();
            return result;
        }
    }
}