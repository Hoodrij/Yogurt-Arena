using Cysharp.Threading.Tasks;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async UniTask<BulletAspect> Run(Asset<BulletView> asset, Entity owner)
        {
            BulletView view = await asset.Spawn();

            BodyState ownerBody = owner.Get<BodyState>();
            Vector3 position = ownerBody.Position.WithY(ownerBody.Position.y + 0.3f);

            Entity entity = World.Create()
                .AddDisposable(view)
                .Add(new BulletState
                {
                    Owner = owner,
                    RigidBody = view.Body
                })
                .Add(new BodyState
                {
                    Position = position
                });
            
            // entity.Run(new BulletUpdateViewJob());

            view.transform.position = position;
            
            return entity.As<BulletAspect>();
        }
    }
}