using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            ChargeConfig config = Query.Single<ChargeConfig>();

            ItemAspect weapon = await new ItemFactoryJob().Run(config, owner); 
            weapon.Add(owner.BattleState);

            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);

            return weapon;
        }
    }
}