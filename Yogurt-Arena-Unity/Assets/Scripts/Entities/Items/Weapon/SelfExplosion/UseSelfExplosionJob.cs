using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct UseSelfExplosionJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            SelfExplosionConfig config = item.Get<SelfExplosionConfig>();
            AgentAspect owner = item.Owner;

            item.Run(FireLoop);
            return;
            

            async UniTask FireLoop()
            {
                await new WaitForWeaponReadyJob().Run(item);

                new DealAoeDamageJob().Run(owner, owner.Body.Position, config.Explosion.Damage);
                new SpawnExplosionJob().Run(config.Explosion, owner.Body.Position);
                
                await Wait.Seconds(config.Weapon.Cooldown, item.Life());
            }
        }
    }
}