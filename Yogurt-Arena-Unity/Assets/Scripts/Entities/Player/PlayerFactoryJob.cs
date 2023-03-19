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
            player.Items.Items.Add(await new RifleFactoryJob().Run());
            
            foreach (ItemAspect itemAspect in player.Items.Items)
            {
                itemAspect.Item.Job.Run(player.Entity);
            }

            return player.Entity;
        }
    }
}