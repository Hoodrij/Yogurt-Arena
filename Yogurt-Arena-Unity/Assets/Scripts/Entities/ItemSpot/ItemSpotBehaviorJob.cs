using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async UniTask Run(ItemSpotAspect itemSpot)
        {
            itemSpot.Run(Update);
            return;


            async UniTask Update()
            {
                ItemType itemType = await WaitForActivation();

                itemSpot.View.Show(itemType);
                AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
                await new GiveItemJob().Run(itemType, agent);

                itemSpot.View.Hide();
                itemSpot.State.Type = ItemType.Empty;
            }
            async UniTask<ItemType> WaitForActivation()
            {
                await Wait.Until(() => itemSpot.State.Type != ItemType.Empty);
                return itemSpot.State.Type;
            }
            
        }
    }
}