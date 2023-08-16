using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct LevelUpJob
    {
        public async UniTask Run()
        {
            Level level = Query.Single<Level>();
            level.Current += 1;
            
            await new SpawnNextLocationPartJob().Run();
        }
    }
}