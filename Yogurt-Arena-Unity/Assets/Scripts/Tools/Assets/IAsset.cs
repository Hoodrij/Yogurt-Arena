using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public interface IAsset<TComponent> where TComponent : Component
    {
        public UniTask<TComponent> Spawn();
    }
}