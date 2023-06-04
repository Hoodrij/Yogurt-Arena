using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct PickupWeaponQuest : IQuest
    {
        public async UniTask Run()
        {
            PlayerAspect player = Query.Single<PlayerAspect>();
            await new WaitForEntityChanged().Run(() => player.Agent.Inventory.Weapon.Entity);
        }
    }
}