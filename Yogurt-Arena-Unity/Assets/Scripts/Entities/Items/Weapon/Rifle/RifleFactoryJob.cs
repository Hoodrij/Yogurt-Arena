using UnityEngine;

namespace Yogurt.Arena
{
    public class RifleFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RifleConfig config = Query.Single<RifleConfig>();

            ItemAspect weapon = await new ItemFactoryJob().Run(config, owner);
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}