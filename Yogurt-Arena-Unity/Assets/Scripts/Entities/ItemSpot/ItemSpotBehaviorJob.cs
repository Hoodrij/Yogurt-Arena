namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async void Run(ItemSpotAspect itemSpot)
        {
            EItemType itemType = itemSpot.State.Type;

            AgentAspect agentAspect = await new WaitForItemPickupJob().Run(itemSpot);
            ClearOldItems();
            itemType.GetFactoryJob().Run(agentAspect);


            void ClearOldItems()
            {
                foreach (ItemAspect itemAspect in agentAspect.Items.Value)
                {
                    itemAspect.Kill();
                }
                agentAspect.Items.Value.Clear();
            }
        }
    }
}