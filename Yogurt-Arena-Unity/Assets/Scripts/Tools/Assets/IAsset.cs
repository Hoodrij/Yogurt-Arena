using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena.Tools
{
    public interface IAsset<TComponent> where TComponent : Component
    { 
        UniTask<TComponent> Spawn();
    }
}