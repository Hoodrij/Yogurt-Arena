using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena.Quest
{
    public struct KillEnemiesQuest : IQuest
    {
        private int amountToKill;
        public KillEnemiesQuest(int amountToKill)
        {
            this.amountToKill = amountToKill;
        }

        public async Awaitable Run()
        {
            int killedEnemies = 0;
            HashSet<Entity> enemies = new();  

            while (killedEnemies < amountToKill)
            {
                foreach (AgentAspect enemy in Query.Of<AgentAspect>().Without<PlayerTag>())
                {
                    if (enemies.Add(enemy.Entity))
                    {
                        ListenForDeath(enemy.Entity);
                    }
                }

                await Wait.Update();
            }

            return;


            async void ListenForDeath(Entity enemy)
            {
                await enemy.WaitForDead();
                killedEnemies++;
            }   
        }
    }
}