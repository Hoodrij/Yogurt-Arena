namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async void Run(ItemSpotAspect itemSpot)
        {
            EItemType itemType = itemSpot.State.Type;

            AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
            itemType.GetFactoryJob().Run(agent);

            itemSpot.Kill();
        }
    }
}