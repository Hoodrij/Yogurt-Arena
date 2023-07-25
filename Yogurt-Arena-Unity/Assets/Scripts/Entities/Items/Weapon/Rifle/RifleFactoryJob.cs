using UnityEngine;
using Yogurt.Arena.Components;

namespace Yogurt.Arena
{
    public class RifleFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RifleConfig config = Query.Single<Config>().Rifle;

            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseRifleJob(), 
                EItemType.Rifle);
            weapon.Add(config);
            weapon.Add(config.Common);
            weapon.Add(config.Scattering);
            weapon.Add(config.Lifetime);
            weapon.Add(config.TargetDetection);
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}