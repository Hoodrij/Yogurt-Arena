using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct ReloadJob
    {
        public async UniTask Run(ItemAspect weapon)
        {
            WeaponConfig weaponConfig = weapon.Get<WeaponConfig>();

            float reloadTime = weaponConfig.Cooldown; 

            if (weapon.TryGet(out WeaponClipState clipState))
            {
                WeaponClipConfig clipConfig = weapon.Get<WeaponClipConfig>();
                
                clipState.CurrentAmmo--;
                if (clipState.CurrentAmmo <= 0)
                {
                    clipState.CurrentAmmo = clipConfig.BulletsInClip;
                    reloadTime = clipConfig.ClipCooldown;
                }
            }
            
            await Wait.Seconds(reloadTime);
        }
    }
}