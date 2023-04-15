using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async UniTask<BulletAspect> Run(BulletData data, AgentAspect owner)
        {
            BulletView view = await data.Asset.Spawn();

            BulletAspect bullet = World.Create()
                .AddLink(view.gameObject)
                .Add(data)
                .Add(view)
                .Add(new BulletState
                {
                    Owner = owner,
                    RigidBody = view.Body,
                    Collider = view.Collider,
                })
                .As<BulletAspect>();

            return bullet;
        }
    }
}