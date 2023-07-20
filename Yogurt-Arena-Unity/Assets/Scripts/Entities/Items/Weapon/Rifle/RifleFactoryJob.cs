using UnityEngine;
using Yogurt.Arena.Components;

namespace Yogurt.Arena
{
    public class RifleFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RifleData data = Query.Single<Data>().Rifle;

            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseRifleJob(), 
                EItemType.Rifle);
            weapon.Add(data);
            weapon.Add(data.Common);
            weapon.Add(data.Scattering);
            weapon.Add(data.Lifetime);
            weapon.Add(data.TargetDetection);
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}