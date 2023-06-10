using Cysharp.Threading.Tasks;

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
            weapon.Item.Job.Run(weapon);

            await UniTask.Delay(5.ToSeconds());
            if (weapon.Exist())
            {
                weapon.Kill();
            }
        }
    }
}