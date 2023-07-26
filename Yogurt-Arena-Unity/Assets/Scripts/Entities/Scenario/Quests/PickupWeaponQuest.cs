using UnityEngine;

namespace Yogurt.Arena.Quest
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