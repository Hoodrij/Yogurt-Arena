using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            WeaponData data = Query.Single<Data>().Charge;
            
            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseChargeJob(), 
                EItemType.Charge);
            item.Add(data);

            new SetWeaponJob().Run(owner, item);

            return item;
        }
    }
}