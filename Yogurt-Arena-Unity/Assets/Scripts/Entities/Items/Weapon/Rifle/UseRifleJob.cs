using Cysharp.Threading.Tasks;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class UseRifleJob : ItemUseJob
    {
        public async UniTask Run(Entity owner)
        {
            Asset<BulletView> asset = Query.Single<Assets>().RifleBullet;

            while (owner.Exist)
            {
                Vector3 position = owner.Get<BodyState>().Position;
                new BulletFactoryJob().Run(asset, position);

                await UniTask.Delay(1.Seconds());
            }
        }
    }
}