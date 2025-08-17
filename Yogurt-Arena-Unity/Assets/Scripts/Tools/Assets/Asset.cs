using Object = UnityEngine.Object;

namespace Yogurt.Arena.Tools;

[Serializable]
public class Asset<TComponent> : IAsset<TComponent> where TComponent : Component
{
    public GameObject Prefab;

    public async UniTask<TComponent> Spawn()
    {
        TComponent result = Object.Instantiate(Prefab).GetComponent<TComponent>();
        return result;
    }
}