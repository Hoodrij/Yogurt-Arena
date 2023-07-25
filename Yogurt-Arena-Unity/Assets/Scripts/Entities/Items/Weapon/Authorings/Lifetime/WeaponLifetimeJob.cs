namespace Yogurt.Arena
{
    public struct WeaponLifetimeJob
    {
        public async void Run(ItemAspect item)
        {
            AgentAspect owner = item.Owner.Owner;

            if (!owner.Has<PlayerTag>())
                return;

            ItemLifetimeConfig lifetimeConfig = item.Get<ItemLifetimeConfig>();
            await Wait.Seconds(lifetimeConfig.LifeTime);
            
            if (owner.Exist())
            {
                if (item.Exist())
                {
                    item.Kill();
                }
            }
        }
    }
}