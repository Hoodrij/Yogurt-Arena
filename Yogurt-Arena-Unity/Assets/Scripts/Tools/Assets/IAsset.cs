using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public interface IAsset
    {
        public UniTask<GameObject> Spawn();
    }
    
    public interface IAsset<TComponent> where TComponent : Component
    {
        public UniTask<TComponent> Spawn();
    }
}