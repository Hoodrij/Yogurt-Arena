using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UseChargeJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponData data = item.Get<WeaponData>();
            AgentAspect owner = item.Item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new FireBulletJob().Run(bullet, owner.View.transform.forward * data.Bullet.Speed);
                await new ChargeBehaviorJob().Run(bullet);

                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
        }
    }
}