using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseChargeJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponData data = item.Get<WeaponData>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                if (!item.Exist()) return;
                
                BulletAspect bullet = await new BulletFactoryJob().Run(data.Bullet, owner);
                new FireBulletJob().Run(bullet, default);
                await new ChargeBehaviorJob().Run(bullet);

                await UniTask.Delay(data.Cooldown.ToSeconds());
            }
        }
    }
}