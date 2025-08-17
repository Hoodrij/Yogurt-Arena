namespace Yogurt.Arena
{
    public struct UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect item)
        {
            WeaponConfig config = item.Get<WeaponConfig>();
            RifleConfig rifleConfig = item.Get<RifleConfig>();
            AgentAspect owner = item.Owner;

            item.Run(FireLoop);
            return;


            async UniTask FireLoop()
            {
                await new WaitForWeaponReadyJob().Run(item);
                for (int i = 0; i < rifleConfig.BulletsInShot; i++)
                {
                    FireBullet();
                }
                await new ReloadJob().Run(item);
            }
            async void FireBullet()
            {
                BulletAspect bullet = await new BulletFactoryJob().Run(config.Bullet, owner);
                new FireBulletJob().Run(bullet, GetVelocity(bullet));
                new RifleBulletBehaviorJob().Run(bullet).Forget();
            }
            Vector3 GetVelocity(BulletAspect bullet)
            {
                BodyState targetBody = owner.BattleState.Target.Body;
                Vector3 dir = (targetBody.MiddlePoint - owner.Body.MiddlePoint).normalized;

                Vector3 velocity = dir * bullet.Config.Speed;
                velocity = new ApplyScatteringJob().Run(item, velocity);

                return velocity;
            }
        }
    }
}