using UnityEngine;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerFactory
    {
        public async Awaitable Run()
        {
            Entity entity = World.Create()
                .Add<ItemsSpawnerState>();

            new ItemsSpawnerBehaviorJob().Run(entity);
        }
    }
}