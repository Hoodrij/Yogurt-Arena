using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<PlayerAspect> Run()
        {
            AgentConfig config = Query.Single<Config>().Player;

            AgentAspect agent = await new AgentSpawnJob().Run(config, Team.Green, Vector3.zero);
            agent.Add<PlayerTag>();
            agent.Health.HealthWidget = Query.Single<UIView>().HealthWidget;

            new UpdatePlayerDestinationJob().Run(agent.As<PlayerAspect>());

            return agent.As<PlayerAspect>();
        }
    }
}