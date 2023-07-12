﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async Awaitable<BulletAspect> Run(BulletData data, AgentAspect owner)
        {
            BulletView view = await data.Asset.Spawn();

            BulletAspect bullet = World.Create()
                .AddLink(view.gameObject)
                .Add(data)
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