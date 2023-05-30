﻿namespace Yogurt.Arena
{
    public struct ItemSpotAspect : IAspect
    {
        public Entity Entity { get; set; }

        public ItemSpotState State => this.Get<ItemSpotState>();
        public BodyState Body => this.Get<BodyState>();
    }
}