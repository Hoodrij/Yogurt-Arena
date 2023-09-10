using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseChargeJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponConfig config = item.Get<WeaponConfig>();
            AgentAspect owner = item.Owner;

            item.Run(FireLoop);
            return;
            

            async UniTask FireLoop()
            {
                await new WaitForWeaponReadyJob().Run(item);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(config.Bullet, owner);
                new FireBulletJob().Run(bullet, default);
                await new ChargeBehaviorJob().Run(bullet);

                await new ReloadJob().Run(item);
            }
        }
    }
}