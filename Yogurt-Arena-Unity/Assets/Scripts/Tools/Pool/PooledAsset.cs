using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    [Serializable]
    public class PooledAsset<TComponent> : IAsset<TComponent> where TComponent : Component
    {
        public Asset<TComponent> asset;
        private Pool pool;

        public async UniTask<TComponent> Spawn()
        {
            pool ??= new(async () => (await asset.Spawn()).gameObject);
            return (await pool.Pop()).GetComponent<TComponent>();
        }
    }
}