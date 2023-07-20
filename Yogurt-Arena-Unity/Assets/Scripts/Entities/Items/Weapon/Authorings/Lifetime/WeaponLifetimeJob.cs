namespace Yogurt.Arena
{
    public struct WeaponLifetimeJob
    {
        public async void Run(ItemAspect item)
        {
            AgentAspect owner = item.Owner.Owner;

            if (!owner.Has<PlayerTag>())
                return;

            ItemLifetimeData lifetimeData = item.Get<ItemLifetimeData>();
            await Wait.Seconds(lifetimeData.LifeTime);
            
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