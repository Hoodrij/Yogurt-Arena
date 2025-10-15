namespace Yogurt.Arena;

public struct UseBoomerWeaponJob : IItemUseJob
{
    public async UniTask Run(ItemAspect item)
    {
        BoomerWeaponConfig config = item.Get<BoomerWeaponConfig>();
        AgentAspect owner = item.Owner;

        item.Run(FireLoop);
        return;
            

        async UniTask FireLoop()
        {
            await new WaitForWeaponReadyJob().Run(item);

            new DealAoeDamageJob().Run(owner, owner.Body.Position, config.Explosion.Damage);
            new SpawnExplosionJob().Run(config.Explosion, owner.Body.Position).Forget();
                
            await Wait.Seconds(config.Weapon.Cooldown, item.Life());
        }
    }
}