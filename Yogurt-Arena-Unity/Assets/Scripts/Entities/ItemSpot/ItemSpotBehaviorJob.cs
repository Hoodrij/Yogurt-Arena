using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async UniTask Run(ItemSpotAspect itemSpot)
        {
            EItemType itemType = itemSpot.State.Type;

            AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
            itemType.GetFactoryJob().Run(agent);

            itemSpot.Kill();
        }
    }
}