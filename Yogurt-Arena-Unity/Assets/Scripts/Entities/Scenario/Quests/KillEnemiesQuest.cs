using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
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
            int killedEnemies = 0;

            HashSet<Entity> enemies = new();

            do
            {
                foreach (AgentAspect enemy in Query.Of<AgentAspect>().Without<PlayerTag>())
                {
                    if (enemies.Add(enemy.Entity))
                    {
                        ListenForDeath(enemy.Entity);
                    }
                }

                await UniTaskEx.Yield();
            } while (killedEnemies < amountToKill);
            
            
            async void ListenForDeath(Entity enemy)
            {
                await new WaitForEntityDead().Run(enemy);
                killedEnemies++;
            }   
        }
    }
}