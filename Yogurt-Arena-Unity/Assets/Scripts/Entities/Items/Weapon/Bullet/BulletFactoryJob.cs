using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async UniTask<BulletAspect> Run(IAsset<BulletView> asset, Entity owner)
        {
            BulletView view = await asset.Spawn();

            BodyState ownerBody = owner.Get<BodyState>();
            Vector3 position = ownerBody.Position.WithY(ownerBody.Position.y + 0.3f);

            BulletAspect bullet = World.Create()
                .AddLink(view.gameObject)
                .Add(Query.Single<Data>().Bullet)
                .Add(view)
                .Add(new BulletState
                {
                    Owner = owner,
                    RigidBody = view.Body,
                    Collider = view.Collider,
                })
                .As<BulletAspect>();
            
            bullet.State.RigidBody.isKinematic = false;
            bullet.View.transform.position = position;
            bullet.View.transform.DOKill();
            bullet.View.transform.localScale = Vector3.one;
            
            new FireBulletJob().Run(bullet);

            return bullet;
        }
    }
}