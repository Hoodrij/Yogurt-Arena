using System;
using System.Linq;
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
                GetFactory(itemType).Run(agent);

                itemSpot.View.Hide();
                itemSpot.State.Type = ItemType.Empty;
            }
            async UniTask<ItemType> WaitForActivation()
            {
                await Wait.Until(() => itemSpot.State.Type != ItemType.Empty);
                return itemSpot.State.Type;
            }
            IItemFactoryJob GetFactory(ItemType type)
            {
                ItemConfigAspect config = Query.Of<ItemConfigAspect>()
                    .FirstOrDefault(item => item.Config.Type == type);
                
                return config.Config.FactoryJob;
            }
        }
    }
}