using Cysharp.Threading.Tasks;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct BulletFactoryJob
    {
        public async UniTask<BulletAspect> Run(Asset<BulletView> asset, Vector3 position)
        {
            BulletView view = await asset.Spawn();

            Entity entity = World.Create()
                .AddDisposable(view)
                .Add(new BodyState
                {
                    Position = position
                });
            
            entity.Run(new BulletUpdateViewJob());

            return entity.As<BulletAspect>();
        }
    }
}