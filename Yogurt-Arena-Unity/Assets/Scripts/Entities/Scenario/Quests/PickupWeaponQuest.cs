using Cysharp.Threading.Tasks;

namespace Yogurt.Arena.Quest
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