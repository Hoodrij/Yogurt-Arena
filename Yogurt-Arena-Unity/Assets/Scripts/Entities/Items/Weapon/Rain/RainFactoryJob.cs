using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RainData rainData = Query.Single<Data>().Rain;
            WeaponData commonData = rainData.CommonData;

            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseRainJob(), commonData);
            item.Add(rainData);
            item.Add(rainData.ClipData);
            item.Add(new WeaponClipState
            {
                CurrentAmmo = rainData.ClipData.BulletsInClip
            });
            item.Item.Job.Run(item);
            
            return item;
        }
    }
}