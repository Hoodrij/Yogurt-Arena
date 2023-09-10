using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WorldUIFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            UIConfig config = new GetConfigJob().Run<UIConfig>();;
            WorldUIView worldUI = await config.WorldUI.Spawn();

            Entity entity = World.Create()
                .AddLink(worldUI.gameObject)
                .Add(worldUI)
                .Add(config);

            return entity;
        }
    }
}