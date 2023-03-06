using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public interface IAssetLoader
    {
        UniTask<Object> Load(string path);
        UniTask<TComponent> Load<TComponent>(string path) where TComponent : Component;
    }
}