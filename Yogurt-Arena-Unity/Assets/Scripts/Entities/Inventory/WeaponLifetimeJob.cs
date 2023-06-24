namespace Yogurt.Arena
{
    public struct WeaponLifetimeJob
    {
        public async void Run(AgentAspect agent)
        {
            if (!agent.Has<PlayerTag>())
                return;
            
            await Wait.Seconds(5);
            if (agent.Exist())
            {
                ItemAspect weapon = agent.Inventory.Weapon;
                if (weapon.Exist())
                {
                    weapon.Kill();
                }
            }
        }
    }
}