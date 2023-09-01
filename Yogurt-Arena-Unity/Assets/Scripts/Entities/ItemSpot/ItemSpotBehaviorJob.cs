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
                itemSpot.State.IsTaken = true;

                itemSpot.View.Show(itemType);
                AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
                await new GiveItemJob().Run(itemType, agent);

                itemSpot.View.Hide();
                itemSpot.State.Type = ItemType.Empty;

                await Wait.Seconds(1, itemSpot.Entity);
                itemSpot.State.IsTaken = false;
            }
            async UniTask<ItemType> WaitForActivation()
            {
                await Wait.Until(() => itemSpot.State.Type != ItemType.Empty, itemSpot.Entity);
                return itemSpot.State.Type;
            }
            
        }
    }
}