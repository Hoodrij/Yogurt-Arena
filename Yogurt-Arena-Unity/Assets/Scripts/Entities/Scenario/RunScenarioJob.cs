using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunScenarioJob
    {
        public async UniTask Run()
        {
            new HandleGameOverJob().Run();
            
            new RunOvermindBehaviorJob().Run();
            // await new Quest.PickupWeaponQuest().Run();
            // await Wait.Seconds(1);
            
            // await new Quest.KillEnemiesQuest(1).Run();
            // await new LevelUpJob().Run();
            
            // await new Quest.KillEnemiesQuest(10).Run();
            // await new LevelUpJob().Run();
        }
    }
}