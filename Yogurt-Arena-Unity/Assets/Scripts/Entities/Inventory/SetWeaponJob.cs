namespace Yogurt.Arena
{
    public struct SetWeaponJob
    {
        public async void Run(AgentAspect owner, ItemAspect weapon)
        {
            if (owner.Inventory.Weapon.Exist())
            {
                owner.Inventory.Weapon.Kill();
            }
            
            owner.Inventory.Weapon = weapon;
            weapon.Config.UseJob.Run(weapon);

            new WeaponLifetimeJob().Run(weapon);
        }
    }
}