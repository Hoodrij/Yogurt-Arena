using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            AgentAspect agent = await new AgentFactoryJob().Run(assets.Player, Team.Green);
            agent.Add<PlayerTag>();

            return agent.Entity;
        }
    }
}