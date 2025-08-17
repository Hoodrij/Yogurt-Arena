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
            HashSet<Entity> enemies = new();

            world.Run(Update);
            await Wait.While(() => killedEnemies < enemiesToKill, world.Life());
            return;

            void Update()
            {
                foreach (AgentAspect enemy in Query.Of<AgentAspect>().Without<PlayerTag>())
                {
                    if (enemies.Add(enemy.Entity))
                    {
                        ListenForDeath(enemy).Forget();
                    }
                }
            }
            async UniTaskVoid ListenForDeath(AgentAspect enemy)
            {
                await enemy.Life();
                killedEnemies++;
            }   
        }
    }
}