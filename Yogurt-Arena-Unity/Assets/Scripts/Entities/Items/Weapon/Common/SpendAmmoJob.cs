using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SpendAmmoJob
    {
        /// <returns>True if has available ammo and do NOT requires clip reload</returns>
        public async UniTask<bool> Run(WeaponWithClipAspect weapon)
        {
            weapon.State.CurrentAmmo--;
            
            if (weapon.State.CurrentAmmo > 0)
            {
                return true;
            }

            await UniTask.Delay(weapon.Data.ClipCooldown.ToSeconds());
            weapon.State.CurrentAmmo = weapon.Data.BulletsInClip;
            return false;
        }
    }
}