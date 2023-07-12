using UnityEngine;

namespace Yogurt.Arena
{
    public struct StartScenarioJob
    {
        public async Awaitable Run()
        {
            await new PickupWeaponQuest().Run();
            await Wait.Seconds(1);
            new RunOvermindBehaviorJob().Run();
            await new KillEnemiesQuest(3).Run();
            await new PickupWeaponQuest().Run();
            
            // foreach (IQuest quest in Quests())
            // {
            //     await quest.Run();
            //     123.log();
            // }
            //
            //
            // IEnumerable<IQuest> Quests()
            // {
            //     yield return new PickupWeaponQuest();
            //     yield return new KillEnemiesQuest(3);
            //     yield return new PickupWeaponQuest();
            // }
        }
    }
}