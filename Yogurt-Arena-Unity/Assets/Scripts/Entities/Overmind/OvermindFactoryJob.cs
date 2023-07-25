using UnityEngine;

namespace Yogurt.Arena
{
    public struct OvermindFactoryJob
    {
        public async Awaitable<OvermindAspect> Run()
        {
            OvermindAspect overmind = World.Create()
                .Add(Query.Single<Config>().Overmind)
                .Add<OvermindState>()
                .As<OvermindAspect>();

            return overmind;
        }
    }
}