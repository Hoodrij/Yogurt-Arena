using System.Threading.Tasks;
using NUnit.Framework;
using Random = UnityEngine.Random;

namespace Yogurt.Arena
{
    public class Test_N_Enemies
    {
        [TestCase(1)]
        public async Task Run(int count)
        {
            Random.InitState(seed: 1234);
            
            await new GameFactoryJob().Run();
            await new WorldFactoryJob().Run();

            await GiveWeapon();
            await SpawnEnemiesAndWaitForKill();

            
            async Task GiveWeapon()
            {
                AgentAspect player = Query.Of<PlayerAspect>().Single().Agent;
                await new GiveItemJob().Run(ItemType.Rifle, player);
            }
            async Task SpawnEnemiesAndWaitForKill()
            {
                OvermindAspect overmind = Query.Single<OvermindAspect>();
                AgentAspect player = Query.Of<PlayerAspect>().Single().Agent;
                
                for (int i = 0; i < count; i++)
                {
                    AgentAspect enemy = await new SpawnSingleEnemyJob().Run(overmind);
                    enemy.Body.Position = player.Body.Position + Random.insideUnitCircle.ToV3XZ() * 5;
                }
                await Wait.While(() => overmind.State.CurrentAgents.Count > 0);
            }
        } 
    }
}