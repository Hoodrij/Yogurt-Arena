using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpendAmmoJob
    {
        /// <returns>True if has available ammo and do NOT require clip reload</returns>
        public async UniTask<bool> Run(WeaponWithClipAspect weapon)
        {
            weapon.State.CurrentAmmo--;
            
            if (weapon.State.CurrentAmmo > 0)
            {
                return true;
            }

            await Wait.Seconds(weapon.Config.ClipCooldown);
            weapon.State.CurrentAmmo = weapon.Config.BulletsInClip;
            return false;
        }
    }
}