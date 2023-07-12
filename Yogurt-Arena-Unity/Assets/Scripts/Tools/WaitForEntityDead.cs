using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForEntityDead
    {
        public async Awaitable Run(Entity entity)
        {
            await Wait.While(() => entity.Exist);
        }
    }
}