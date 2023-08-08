using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena.Tools
{
    public interface IAsset<TComponent> where TComponent : Component
    {
        public UniTask<TComponent> Spawn();
    }
}