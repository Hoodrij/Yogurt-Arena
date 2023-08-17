using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunScenarioJob
    {
        public async UniTask Run()
        {
            await new Quest.PickupWeaponQuest().Run();
            await new LevelUpJob().Run();
            
            new RunOvermindBehaviorJob().Run();
            await new Quest.KillEnemiesQuest(3).Run();
            await new LevelUpJob().Run();
        }
    }
}