using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RainData rainData = Query.Single<Data>().Rain;

            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseRainJob(), 
                EItemType.Rain);
            weapon.Add(rainData);
            weapon.Add(rainData.Common);
            weapon.Add(rainData.Scattering);
            weapon.Add(rainData.Clip);
            weapon.Add(rainData.Lifetime);
            weapon.Add(rainData.TargetDetection);
            weapon.Add(new WeaponClipState
            {
                CurrentAmmo = rainData.Clip.BulletsInClip
            });
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}