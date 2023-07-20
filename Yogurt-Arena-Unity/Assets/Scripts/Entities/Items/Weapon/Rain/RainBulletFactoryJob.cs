using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainBulletFactoryJob
    {
        public async Awaitable<BulletAspect> Run(BulletData data, RainData rainData, AgentAspect owner)
        {
            BulletAspect bullet = await new BulletFactoryJob().Run(data, owner);
            bullet.Add(owner.BattleState);
            bullet.Add(new OwnerState
            {
                Owner = owner
            });
            bullet.Add(rainData.Bullet);

            return bullet;
        }
    }
}