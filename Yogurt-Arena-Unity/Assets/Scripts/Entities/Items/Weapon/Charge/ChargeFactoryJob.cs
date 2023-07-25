using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            ChargeConfig config = Query.Single<Config>().Charge;
            
            ItemAspect weapon = await new ItemFactoryJob().Run(owner, 
                new UseChargeJob(), 
                EItemType.Charge);
            weapon.Add(config);
            weapon.Add(config.Common);
            weapon.Add(config.Lifetime);
            weapon.Add(config.TargetDetection);
            weapon.Add(owner.BattleState);

            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);

            return weapon;
        }
    }
}