using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct LevelFactoryJob
    {
        public async Awaitable Run()
        {
            GameObject levelGO = new GameObject("Level");
            Level levelComp = levelGO.AddComponent<Level>();
            NavMeshSurface navMeshSurface = levelGO.AddComponent<NavMeshSurface>();
            levelComp.NavSurface = navMeshSurface;

            levelComp.NavSurface.collectObjects = CollectObjects.Children;
            levelComp.NavSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;

            World.Create()
                .AddLink(levelGO)
                .Add(levelComp);
            
            await new SpawnNextLevelPartJob().Run();
        }
    }
}