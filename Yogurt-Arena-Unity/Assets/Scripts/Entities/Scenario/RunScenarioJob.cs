using UnityEngine;

namespace Yogurt.Arena
{
    public struct RunScenarioJob
    {
        public async Awaitable Run()
        {
            new RunOvermindBehaviorJob().Run();
            await new Quest.PickupWeaponQuest().Run();
            await Wait.Seconds(1);
            
            await new Quest.KillEnemiesQuest(3).Run();
            await new SpawnNextLevelPartJob().Run();
        }
    }
}