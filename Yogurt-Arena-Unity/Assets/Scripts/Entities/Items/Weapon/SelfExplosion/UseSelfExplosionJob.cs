using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseSelfExplosionJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            SelfExplosionConfig config = item.Get<SelfExplosionConfig>();
            AgentAspect owner = item.Owner;

            while (item.Exist())
            {
                await new WaitForWeaponReadyJob().Run(item);
                if (!item.Exist()) return;

                new DealAoeDamageJob().Run(owner, owner.Body.Position, config.Explosion.Damage);
                new SpawnExplosionJob().Run(config.Explosion, owner.Body.Position);
                
                await Wait.Seconds(config.Weapon.Cooldown, item.Entity);
            }
        }
    }
}