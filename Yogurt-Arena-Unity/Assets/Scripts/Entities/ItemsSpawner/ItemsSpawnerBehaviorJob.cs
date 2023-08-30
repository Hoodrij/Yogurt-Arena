using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemsSpawnerBehaviorJob
    {
        public async UniTask Run(ItemSpawnerAspect itemSpawner)
        {
            itemSpawner.Run(Update);
            return;


            async UniTask Update()
            {
                await WaitForSpawnAvailable();
                await Wait.Seconds(0.5f);
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = new GetRandomItemJob().Run(itemSpawner.Config.AvailableTags, itemSpawner.Config.AvailableItems);
            }
            async UniTask WaitForSpawnAvailable()
            {
                await Wait.While(() =>
                {
                    return Query.Of<ItemSpotAspect>()
                        .Count(itemSpot => itemSpot.Get<ItemSpotState>().Type != ItemType.Empty) >= itemSpawner.Config.ItemsCount;
                }, itemSpawner.Entity);
            }
            IEnumerable<ItemSpotAspect> GetFreeSpots()
            {
                return Query.Of<ItemSpotAspect>()
                    .Where(itemSpot => !itemSpot.Get<ItemSpotState>().IsTaken);
            }
        }
    }
}