using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct LevelUpJob
    {
        public async UniTask Run()
        {
            Level level = Query.Single<Level>();
            
            await new SpawnLocationPartJob().Run(level + 1);
            
            level.Current += 1;
        }
    }
}