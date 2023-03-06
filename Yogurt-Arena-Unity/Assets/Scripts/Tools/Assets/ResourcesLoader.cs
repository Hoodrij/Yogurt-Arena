using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Roguelike.Tools
{
    public class ResourcesLoader : IAssetLoader
    {
        public static IAssetLoader Instance { get; } = new ResourcesLoader();
        
        public async UniTask<Object> Load(string path)
        {
            return await Resources.LoadAsync(path);
        }

        public async UniTask<TComponent> Load<TComponent>(string path) where TComponent : Component
        {
            GameObject loaded = (GameObject) await Load(path);
            return loaded.GetComponent<TComponent>();
        }
    }
}