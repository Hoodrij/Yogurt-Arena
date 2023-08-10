using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UIFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            UIConfig config = Query.Single<Config>().UI;
            UIView uiView = await config.Asset.Spawn();

            Entity entity = World.Create()
                .AddLink(uiView.gameObject)
                .Add(uiView)
                .Add(config);

            return entity;
        }
    }
}