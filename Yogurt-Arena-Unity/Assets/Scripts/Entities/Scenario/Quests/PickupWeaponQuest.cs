using UnityEngine;

namespace Yogurt.Arena
{
    public struct PickupWeaponQuest : IQuest
    {
        public async Awaitable Run()
        {
            PlayerAspect player = Query.Single<PlayerAspect>();
            await new WaitForEntityChanged().Run(() => player.Agent.Inventory.Weapon.Entity);
        }
    }
}