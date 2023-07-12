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

            player.Health.Job = new UpdatePlayerHealthUIJob();

            return player.Entity;
        }
    }
}