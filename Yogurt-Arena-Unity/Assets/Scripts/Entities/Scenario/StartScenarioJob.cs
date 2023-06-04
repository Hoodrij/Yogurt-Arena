using System.Collections.Generic;

namespace Yogurt.Arena
{
    public struct StartScenarioJob
    {
        public async void Run()
        {
            foreach (IQuest quest in Quests())
            {
                await quest.Run();
                123.log();
            }


            IEnumerable<IQuest> Quests()
            {
                yield return new PickupWeaponQuest();
                yield return new PickupWeaponQuest();
                yield return new PickupWeaponQuest();
            }
        }
    }
}