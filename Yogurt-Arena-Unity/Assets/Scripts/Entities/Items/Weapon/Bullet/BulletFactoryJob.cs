namespace Yogurt.Arena;

public struct BulletFactoryJob
{
    public async UniTask<BulletAspect> Run(BulletConfig config, AgentAspect owner)
    {
        BulletView view = await config.Asset.Spawn();

        BulletAspect bullet = World.Create()
            .Link(view.gameObject)
            .Add(config)
            .Add(view)
            .Add(new BodyState())
            .Add(new OwnerState
            {
                Value = owner
            })
            .As<BulletAspect>();

        return bullet;
    }
}