namespace Yogurt.Arena
{
    public struct UseRainJob : IItemUseJob
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

                BulletAspect bullet = await new RainBulletFactoryJob().Run(config.Bullet, item.Get<RainConfig>(), owner);
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RainBulletBehaviorJob().Run(bullet.As<RainBulletAspect>()).Forget();
                
                await new ReloadJob().Run(item);
            }
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.Position.WithY(0) - owner.Body.Position.WithY(0))
                    .normalized
                    .WithY(5)
                    .normalized;

                Vector3 velocity = dir * bullet.Config.Speed;
                velocity = new ApplyScatteringJob().Run(item, velocity);

                return velocity;
            }
        }
    }
}