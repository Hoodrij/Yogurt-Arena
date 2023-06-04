namespace Yogurt.Arena
{
    public struct SetWeaponJob
    {
        public void Run(AgentAspect owner, ItemAspect weapon)
        {
            if (owner.Inventory.Weapon.Exist())
            {
                owner.Inventory.Weapon.Kill();
            }
            
            owner.Inventory.Weapon = weapon;
            weapon.Item.Job.Run(weapon);
        }
    }
}