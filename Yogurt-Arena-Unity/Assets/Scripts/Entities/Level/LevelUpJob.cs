using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct LevelUpJob
    {
        public async UniTask Run(float delay = 0)
        {
            Level level = Query.Single<Level>();
            
            await new SpawnLocationPartJob().Run(level + 1);
            // await Wait.Seconds(delay);
            
            level.Current += 1;
        }
    }
}