using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena.Quest
{
    public struct KillEnemiesQuest : IQuest
    {
        private int amountToKill;
        public KillEnemiesQuest(int amountToKill)
        {
            this.amountToKill = amountToKill;
        }

        public async UniTask Run()
        {
            Entity world = Query.Of<World>().Single();
            int killedEnemies = 0;
            int enemiesToKill = amountToKill;
            
            Query.Of<AgentAspect>().Without<PlayerTag>().ListenAdded(OnEnemyAdded);

            await Wait.While(() => killedEnemies < enemiesToKill, world);
            return;

            async UniTaskVoid OnEnemyAdded(AgentAspect enemy)
            {
                await enemy.Entity.WaitForDead();
                killedEnemies++;
            }   
        }
    }
}