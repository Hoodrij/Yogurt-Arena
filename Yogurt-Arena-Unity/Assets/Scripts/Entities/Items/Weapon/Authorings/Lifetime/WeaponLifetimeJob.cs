namespace Yogurt.Arena
{
    public struct WeaponLifetimeJob
    {
        public async void Run(ItemAspect item)
        {
            Time time = Query.Single<Time>();
            WeaponLifetimeWidget widget = Query.Single<UIView>().WeaponLifetimeWidget;
            
            AgentAspect owner = item.Owner.Owner;
            ItemLifetimeConfig lifetimeConfig = item.Get<ItemLifetimeConfig>();

            if (!owner.Has<PlayerTag>())
                return;

            float timeRemains = lifetimeConfig.LifeTime;
            
            item.Run(Update);
            return;


            void Update()
            {
                timeRemains -= time.Delta;
                float progress = timeRemains / lifetimeConfig.LifeTime;
                widget.SetProgress(progress);
                
                if (timeRemains <= 0)
                {
                    item.Kill();
                }
            }
        }
    }
}