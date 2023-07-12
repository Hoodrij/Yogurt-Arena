using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainBulletFactoryJob
    {
        public async Awaitable<BulletAspect> Run(BulletData data, RainData rainData, AgentAspect owner)
        {
            BulletAspect bullet = await new BulletFactoryJob().Run(data, owner);
            bullet.Add(new BattleState
            {
                Target = owner.BattleState.Target
            });
            bullet.Add(new OwnerState
            {
                Owner = owner
            });
            bullet.Add(rainData.BulletData);

            return bullet;
        }
    }
}