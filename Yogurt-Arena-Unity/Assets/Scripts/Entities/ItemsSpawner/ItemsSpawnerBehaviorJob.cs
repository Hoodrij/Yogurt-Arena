using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerBehaviorJob
    {
        public async Awaitable Run(Entity itemSpawner)
        {
            itemSpawner.Run(Update);
            return;


            async Awaitable Update()
            {
                await WaitForSpawnAvailable();
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = new GetRandomItemJob().Run(EItemTags.AvailableToPlayer);
            }
            async Awaitable WaitForSpawnAvailable()
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