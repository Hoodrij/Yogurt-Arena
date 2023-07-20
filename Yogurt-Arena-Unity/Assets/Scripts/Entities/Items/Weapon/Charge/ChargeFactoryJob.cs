using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            ChargeData data = Query.Single<Data>().Charge;
            
            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseChargeJob(), 
                EItemType.Charge);
            weapon.Add(data);
            weapon.Add(data.Common);
            weapon.Add(data.Lifetime);
            weapon.Add(data.TargetDetection);
            weapon.Add(owner.BattleState);

            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);

            return weapon;
        }
    }
}