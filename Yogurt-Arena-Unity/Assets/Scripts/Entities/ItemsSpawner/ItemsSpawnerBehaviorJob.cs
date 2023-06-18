using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerBehaviorJob
    {
        public async UniTask Run(Entity itemSpawner)
        {
            var allSpots = Query.Of<ItemSpotAspect>();
            itemSpawner.Run(Update);

            
            async UniTask Update()
            {
                await WaitForSpawnAvailable();
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = EItemType.Empty.GetRandom(EItemTags.Weapon | EItemTags.PlayerUsed);

                await UniTaskEx.Yield();
            }
            async UniTask WaitForSpawnAvailable()
            {
                await UniTask.WaitWhile(() => 
                        Query.Of<ItemSpotAspect>()
                        .Count(itemSpot => itemSpot.State.Type != EItemType.Empty) >= 2)
                    .AttachLifetime();
            }
            IEnumerable<ItemSpotAspect> GetFreeSpots()
            {
                return allSpots
                    .Where(itemSpot => itemSpot.State.Type == EItemType.Empty);
            }
        }
    }
}