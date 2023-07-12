using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async Awaitable Run()
        {
            Data data = Resources.Load<Data>("Data");

            Entity.Create()
                .Add<Game>()
                .Add(data)
                .Add<Time>();
        }
    }
}