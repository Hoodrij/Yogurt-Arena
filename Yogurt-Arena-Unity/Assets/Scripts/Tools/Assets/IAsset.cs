namespace Yogurt.Arena.Tools;

public interface IAsset<TComponent> where TComponent : Component
{ 
    UniTask<TComponent> Spawn();
}