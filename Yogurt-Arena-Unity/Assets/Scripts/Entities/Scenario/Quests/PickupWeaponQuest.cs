using Cysharp.Threading.Tasks;

namespace Yogurt.Arena.Quest
{
    public struct PickupWeaponQuest : IQuest
    {
        public async UniTask Run()
        {
            PlayerAspect player = Query.Single<PlayerAspect>();
            ItemAspect currentWeapon = player.Agent.Inventory.Weapon;

            await Wait.While(IsWeaponChanged, player.Life());


            bool IsWeaponChanged()
            {
                return currentWeapon.Entity == player.Agent.Inventory.Weapon.Entity;
            }
        }
    }
}