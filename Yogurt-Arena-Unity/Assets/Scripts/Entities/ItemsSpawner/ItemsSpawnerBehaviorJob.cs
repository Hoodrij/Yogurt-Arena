using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerBehaviorJob
    {
        public async UniTask Run(Entity itemSpawner)
        {
            itemSpawner.Run(Update);
            return;


            async UniTask Update()
            {
                await WaitForSpawnAvailable();
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = new GetRandomItemJob().Run(EItemTags.AvailableToPlayer);
            }
            async UniTask WaitForSpawnAvailable()
            {
                await Wait.While(() =>
                {
                    return Query.Of<ItemSpotAspect>()
                        .Count(itemSpot => itemSpot.Get<ItemSpotState>().Type != EItemType.Empty) >= 2;
                });
            }
            IEnumerable<ItemSpotAspect> GetFreeSpots()
            {
                return Query.Of<ItemSpotAspect>()
                    .Where(itemSpot => itemSpot.Get<ItemSpotState>().Type == EItemType.Empty);
            }
        }
    }
}