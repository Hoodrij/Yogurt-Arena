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
            
            player.Items.List.Add(await new RifleFactoryJob().Run());
            foreach (ItemAspect itemAspect in player.Items.List)
            {
                itemAspect.Item.Job.Run(itemAspect, player.Entity);
            }

            return player.Entity;
        }
    }
}