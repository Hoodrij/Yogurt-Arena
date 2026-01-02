namespace Yogurt.Arena;

public struct WeaponLifetimeJob
{
    public void Run(ItemAspect item)
    {
        Time time = Query.Single<Time>();
        AgentAspect owner = item.Owner.Value;
            
        if (!owner.Has<PlayerTag>())
            return;
            
        WeaponLifetimeWidget widget = Query.Single<UIView>().WeaponLifetimeWidget;
        ItemLifetimeConfig lifetimeConfig = item.Get<ItemLifetimeConfig>();

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