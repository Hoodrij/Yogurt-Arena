using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunScenarioJob
    {
        public async UniTask Run()
        {
            await new Quest.PickupWeaponQuest().Run();
            new RunOvermindBehaviorJob().Run();
            
            await new Quest.KillEnemiesQuest(1).Run();
            await new LevelUpJob().Run();
            
            await new Quest.KillEnemiesQuest(3).Run();
            await new LevelUpJob().Run();
        }
    }
}