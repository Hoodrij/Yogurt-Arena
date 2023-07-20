using UnityEngine;

namespace Yogurt.Arena
{
    public struct RainFactoryJob : IItemFactoryJob
    {
        public async Awaitable<ItemAspect> Run(AgentAspect owner)
        {
            RainData rainData = Query.Single<Data>().Rain;

            ItemAspect item = await new ItemFactoryJob().Run(owner, 
                new UseRainJob(), 
                EItemType.Rain);
            item.Add(rainData);
            item.Add(rainData.Common);
            item.Add(rainData.Scattering);
            item.Add(rainData.Clip);
            item.Add(rainData.Lifetime);
            item.Add(new WeaponClipState
            {
                CurrentAmmo = rainData.Clip.BulletsInClip
            });
            
            new SetWeaponJob().Run(owner, item);
            
            return item;
        }
    }
}