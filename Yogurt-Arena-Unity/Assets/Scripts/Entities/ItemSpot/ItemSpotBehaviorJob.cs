﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct ItemSpotBehaviorJob
    {
        public async Awaitable Run(ItemSpotAspect itemSpot)
        {
            itemSpot.Run(Update);


            async Awaitable Update()
            {
                EItemType itemType = await WaitForActivation();

                itemSpot.View.Show(itemType);
                AgentAspect agent = await new WaitForItemPickupJob().Run(itemSpot);
                itemType.GetFactoryJob().Run(agent);

                itemSpot.View.Hide();
                itemSpot.State.Type = EItemType.Empty;
            }
            async Awaitable<EItemType> WaitForActivation()
            {
                await Wait.Until(() => itemSpot.State.Type != EItemType.Empty);
                return itemSpot.State.Type;
            }
        }
    }
}