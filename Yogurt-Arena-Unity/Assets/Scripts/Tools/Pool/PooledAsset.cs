using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public class PooledAsset<TComponent> : IAsset<TComponent> where TComponent : Component
    {
        private Asset asset;
        private Pool pool;

        public PooledAsset(string path)
        {
            asset = new(path);
            pool = new(() => asset.Spawn());
        }

        public async UniTask<TComponent> Spawn()
        {
            return (await pool.Pop()).GetComponent<TComponent>();
        }
    }
}