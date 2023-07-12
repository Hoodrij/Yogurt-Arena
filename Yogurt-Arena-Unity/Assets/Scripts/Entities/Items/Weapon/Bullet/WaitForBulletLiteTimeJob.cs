using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForBulletLiteTimeJob
    {
        public async Awaitable Run(BulletAspect bullet)
        {
            await Wait.Seconds(bullet.Data.LifeTime);
        }   
    }
}