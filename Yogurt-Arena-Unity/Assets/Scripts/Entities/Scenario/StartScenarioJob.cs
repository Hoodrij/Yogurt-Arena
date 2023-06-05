using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct StartScenarioJob
    {
        public async UniTask Run()
        {
            await new PickupWeaponQuest().Run();
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