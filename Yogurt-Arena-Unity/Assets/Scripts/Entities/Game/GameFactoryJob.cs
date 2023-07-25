using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async Awaitable Run()
        {
            Config config = Resources.Load<Config>("Config");

            Entity.Create()
                .Add<Game>()
                .Add(config)
                .Add<Time>();
        }
    }
}