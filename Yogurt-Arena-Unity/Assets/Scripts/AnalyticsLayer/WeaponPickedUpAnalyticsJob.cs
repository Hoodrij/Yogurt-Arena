namespace Yogurt.Arena.Analytics
{
    public struct WeaponPickedUpAnalyticsJob
    {
        public void Run()
        {
            Query.Of<PlayerAspect>()
                .ListenAdded(OnPlayerAdded);
            
            
            void OnPlayerAdded(PlayerAspect player)
            {
                Observable<Entity> observer = new(() => player.Agent.Inventory.Weapon.Entity);
                observer.OnChanged.Listen(_ => new FireAnalyticsEventJob().Run());
            }
        }
    }
}