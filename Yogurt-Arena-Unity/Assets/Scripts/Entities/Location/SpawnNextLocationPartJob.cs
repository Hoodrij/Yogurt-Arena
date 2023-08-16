using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpawnNextLocationPartJob
    {
        public async UniTask Run()
        {
            LocationAspect location = Query.Single<LocationAspect>();
            LocationConfig config = new GetConfigJob().Run<LocationConfig>();

            LocationPartTag locationPart = await config.Asset.Spawn();
            
            locationPart.transform.SetParent(location.NavSurface.transform);
            location.Entity.AddLink(locationPart.gameObject);
            
            location.NavSurface.BuildNavMesh();
        }
    }
}