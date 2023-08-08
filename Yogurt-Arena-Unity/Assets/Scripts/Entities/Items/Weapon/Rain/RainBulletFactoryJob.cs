using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainBulletFactoryJob
    {
        public async UniTask<BulletAspect> Run(BulletConfig config, RainConfig rainConfig, AgentAspect owner)
        {
            BulletAspect bullet = await new BulletFactoryJob().Run(config, owner);
            bullet.Add(owner.BattleState);
            bullet.Add(new OwnerState
            {
                Owner = owner
            });
            bullet.Add(rainConfig.Bullet);

            return bullet;
        }
    }
}