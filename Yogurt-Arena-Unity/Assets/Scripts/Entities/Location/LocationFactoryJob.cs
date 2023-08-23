using Cysharp.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct LocationFactoryJob
    {
        public async UniTask Run()
        {
            GameObject levelGO = new GameObject("Level");
            Location locationComp = levelGO.AddComponent<Location>();
            NavMeshSurface navMeshSurface = levelGO.AddComponent<NavMeshSurface>();
            locationComp.NavSurface = navMeshSurface;

            locationComp.NavSurface.collectObjects = CollectObjects.Children;
            locationComp.NavSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;

            World.Create()
                .AddLink(levelGO)
                .Add(locationComp);
            
            await new SpawnLocationPartJob().Run(0);
        }
    }
}