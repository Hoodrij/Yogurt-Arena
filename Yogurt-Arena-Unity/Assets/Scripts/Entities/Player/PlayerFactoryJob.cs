using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            Assets assets = Query.Single<Assets>();
            AgentAspect player = await new AgentFactoryJob().Run(assets.Player, Team.Green);
            player.Add<PlayerTag>();
            player.Health.Value = 100;

            player.Items.Add(await new RifleFactoryJob().Run(player));

            return player.Entity;
        }
    }
}