using UnityEngine;
using Yogurt.Arena.Components;

namespace Yogurt.Arena
{
    public class RifleFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RifleData data = Query.Single<Data>().Rifle;

            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseRifleJob(), 
                EItemType.Rifle);
            item.Add(data);
            item.Add(data.Common);
            item.Add(data.Scattering);
            item.Add(data.Lifetime);
            
            new SetWeaponJob().Run(owner, item);
            
            return item;
        }
    }
}