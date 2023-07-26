﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct RunScenarioJob
    {
        public async Awaitable Run()
        {
            await new Quest.PickupWeaponQuest().Run();
            await Wait.Seconds(1);
            new RunOvermindBehaviorJob().Run();
            await new Quest.KillEnemiesQuest(3).Run();
            await new Quest.PickupWeaponQuest().Run();
        }
    }
}