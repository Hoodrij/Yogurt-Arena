using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RainData rainData = Query.Single<Data>().Rain;

            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseRainJob());
            item.Add(rainData);
            item.Add(rainData.CommonData);
            item.Add(rainData.ScatteringData);
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