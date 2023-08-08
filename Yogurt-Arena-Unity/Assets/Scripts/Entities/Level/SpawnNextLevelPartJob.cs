using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpawnNextLevelPartJob
    {
        public async UniTask Run()
        {
            Config config = Query.Single<Config>();
            LevelAspect level = Query.Single<LevelAspect>();
            
            LevelPartTag levelPart = await config.Levels[level.Level.CurrentPart].Spawn();
            level.Level.CurrentPart += 1;

            levelPart.transform.SetParent(level.NavSurface.transform);
            level.Entity.AddLink(levelPart.gameObject);
            
            level.NavSurface.BuildNavMesh();
        }
    }
}