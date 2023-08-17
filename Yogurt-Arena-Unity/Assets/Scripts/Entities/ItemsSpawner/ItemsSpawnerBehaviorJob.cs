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
                ItemSpotAspect randomSpot = GetFreeSpots().GetRandom();
                randomSpot.State.Type = new GetRandomItemJob().Run(itemSpawner.Config.ForceTags, itemSpawner.Config.ForceItem);
            }
            async UniTask WaitForSpawnAvailable()
            {
                await Wait.While(() =>
                {
                    return Query.Of<ItemSpotAspect>()
                        .Count(itemSpot => itemSpot.Get<ItemSpotState>().Type != ItemType.Empty) >= itemSpawner.Config.ItemsCount;
                });
            }
            IEnumerable<ItemSpotAspect> GetFreeSpots()
            {
                return Query.Of<ItemSpotAspect>()
                    .Where(itemSpot => itemSpot.Get<ItemSpotState>().Type == ItemType.Empty);
            }
        }
    }
}