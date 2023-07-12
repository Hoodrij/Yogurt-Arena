using UnityEngine;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async Awaitable<Entity> Run()
        {
            AgentData data = Query.Single<Data>().Player;
            
            AgentAspect player = await new AgentSpawnJob().Run(data, Team.Green, Vector3.zero);
            player.Add<PlayerTag>();

            // await new RifleFactoryJob().Run(player);
            // await new RainFactoryJob().Run(player);

            return player.Entity;
        }
    }
}