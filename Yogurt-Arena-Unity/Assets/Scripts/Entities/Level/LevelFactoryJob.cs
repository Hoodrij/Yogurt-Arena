using UnityEngine;

namespace Yogurt.Arena
{
    public struct LevelFactoryJob
    {
        public async Awaitable Run()
        {
            Level view = await Query.Single<Data>().Level.Spawn();

            Entity entity = World.Create()
                .Add(view);
        }
    }
}