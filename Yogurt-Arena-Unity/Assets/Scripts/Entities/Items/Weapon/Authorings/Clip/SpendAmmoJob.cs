using UnityEngine;

namespace Yogurt.Arena
{
    public struct SpendAmmoJob
    {
        /// <returns>True if has available ammo and do NOT require clip reload</returns>
        public async Awaitable<bool> Run(WeaponWithClipAspect weapon)
        {
            weapon.State.CurrentAmmo--;
            
            if (weapon.State.CurrentAmmo > 0)
            {
                return true;
            }

            await Wait.Seconds(weapon.Data.ClipCooldown);
            weapon.State.CurrentAmmo = weapon.Data.BulletsInClip;
            return false;
        }
    }
}