namespace Yogurt.Arena;

public struct LevelUpJob
{
    public async UniTask Run()
    {
        LevelAspect level = Query.Single<LevelAspect>();
            
        await new SpawnLocationPartJob().Run(level.Level.Current + 1);
            
        level.Level.Current += 1;
    }
}