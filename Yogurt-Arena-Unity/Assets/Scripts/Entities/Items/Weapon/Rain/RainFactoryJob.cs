using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RainConfig rainConfig = Query.Single<Config>().Rain;

            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseRainJob(), 
                EItemType.Rain);
            weapon.Add(rainConfig);
            weapon.Add(rainConfig.Common);
            weapon.Add(rainConfig.Scattering);
            weapon.Add(rainConfig.Clip);
            weapon.Add(rainConfig.Lifetime);
            weapon.Add(rainConfig.TargetDetection);
            weapon.Add(new WeaponClipState
            {
                CurrentAmmo = rainConfig.Clip.BulletsInClip
            });
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}