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
                await Wait.Seconds(0.5f, itemSpawner.Life());
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = new GetRandomItemJob().Run(itemSpawner.Config.AvailableTags, itemSpawner.Config.AvailableItems);
            }
            async UniTask WaitForSpawnAvailable()
            {
                await Wait.While(() =>
                {
                    int itemsCount = itemSpawner.Config.ItemsCount;
                    return Query.Of<ItemSpotAspect>()
                        .Count(itemSpot => itemSpot.Get<ItemSpotState>().Type != ItemType.Empty) >= itemsCount;
                }, itemSpawner.Life());
            }
            IEnumerable<ItemSpotAspect> GetFreeSpots()
            {
                return Query.Of<ItemSpotAspect>().AsEnumerable()
                    .Where(itemSpot => itemSpot.Get<ItemSpotState>().Type == ItemType.Empty);
            }
        }
    }
}