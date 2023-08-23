using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpawnLocationPartJob
    {
        public async UniTask Run(int level)
        {
            LocationAspect location = Query.Single<LocationAspect>();
            LocationConfig config = new GetConfigJob().Run<LocationConfig>(level);

            LocationPartTag locationPart = await config.Asset.Spawn();
            
            locationPart.transform.SetParent(location.NavSurface.transform);
            location.Entity.AddLink(locationPart.gameObject);

            if (level > 0)
            {
                await new AnimateLocationAppearanceJob().Run(locationPart);
            }
            
            location.NavSurface.BuildNavMesh();
        }
    }
}