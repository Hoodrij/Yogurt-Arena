using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpawnNextLocationPartJob
    {
        public async UniTask Run()
        {
            Config config = Query.Single<Config>();
            LocationAspect location = Query.Single<LocationAspect>();
            
            LocationPartTag locationPart = await config.Locations[location.Location.CurrentPart].Spawn();
            location.Location.CurrentPart += 1;

            locationPart.transform.SetParent(location.NavSurface.transform);
            location.Entity.AddLink(locationPart.gameObject);
            
            location.NavSurface.BuildNavMesh();
        }
    }
}