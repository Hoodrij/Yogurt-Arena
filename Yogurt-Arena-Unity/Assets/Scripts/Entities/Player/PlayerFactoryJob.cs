using UnityEngine;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async Awaitable<Entity> Run()
        {
            AgentConfig config = Query.Single<Config>().Player;
            
            AgentAspect player = await new AgentSpawnJob().Run(config, Team.Green, Vector3.zero);
            player.Add<PlayerTag>();

            player.Health.HealthWidget = Query.Single<UIView>().HealthWidget;

            return player.Entity;
        }
    }
}