using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UIFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            UIConfig config = new GetConfigJob().Run<UIConfig>();
            UIView uiView = await config.UI.Spawn();

            Entity entity = World.Create()
                .AddLink(uiView.gameObject)
                .Add(uiView)
                .Add(config);

            return entity;
        }
    }
}