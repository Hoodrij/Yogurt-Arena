using UnityEngine;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async Awaitable<BulletAspect> Run(BulletConfig config, AgentAspect owner)
        {
            BulletView view = await config.Asset.Spawn();

            BulletAspect bullet = World.Create()
                .AddLink(view.gameObject)
                .Add(config)
                .Add(view)
                .Add(new BulletState
                {
                    Owner = owner
                })
                .Add<BodyState>()
                .As<BulletAspect>();

            return bullet;
        }
    }
}