using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            AgentData data = Query.Single<Data>().Player;
            
            AgentAspect player = await new AgentFactoryJob().Run(data, Team.Green);
            player.Add<PlayerTag>();
            player.Health.Value = 100;

            player.Items.Add(await new RifleFactoryJob().Run(player));

            return player.Entity;
        }
    }
}