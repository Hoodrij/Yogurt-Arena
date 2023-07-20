using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            ChargeData data = Query.Single<Data>().Charge;
            
            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseChargeJob(), 
                EItemType.Charge);
            item.Add(data);
            item.Add(data.Common);
            item.Add(data.Lifetime);

            new SetWeaponJob().Run(owner, item);

            return item;
        }
    }
}