using System.Linq;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async void Run(ItemSpotAspect itemSpot)
        {
            EItemType itemType = itemSpot.State.Type;
            AgentAspect agent;

            while (itemSpot.Exist())
            {
                agent = await new WaitForItemPickupJob().Run(itemSpot);
                if (!AgentHasItem())
                {
                    ClearOldItems();
                    itemType.GetFactoryJob().Run(agent);
                }

                await UniTask.Yield();
            }


            void ClearOldItems()
            {
                foreach (ItemAspect itemAspect in agent.Items.Value)
                {
                    itemAspect.Kill();
                }
                agent.Items.Value.Clear();
            }

            bool AgentHasItem()
            {
                return agent.Items.Value
                    .Any(item => item.Item.Type == itemType);
            }
        }
    }
}