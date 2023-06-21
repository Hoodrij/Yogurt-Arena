using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async UniTask Run(ItemSpotAspect itemSpot)
        {
            while (itemSpot.Exist())
            {
                EItemType itemType = await WaitForActivation();

                itemSpot.View.Show(itemType);
                AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
                itemType.GetFactoryJob().Run(agent);

                itemSpot.View.Hide();
                itemSpot.State.Type = EItemType.Empty;
            }


            async UniTask<EItemType> WaitForActivation()
            {
                await UniTask.WaitUntil(() => itemSpot.State.Type != EItemType.Empty);
                return itemSpot.State.Type;
            }
        }
    }
}