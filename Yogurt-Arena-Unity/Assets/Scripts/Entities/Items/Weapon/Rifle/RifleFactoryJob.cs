﻿using Cysharp.Threading.Tasks;
using Yogurt.Arena.Components;

namespace Yogurt.Arena
{
    public class RifleFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RifleData data = Query.Single<Data>().Rifle;

            ItemAspect item = await new ItemFactoryJob().Run(owner, new UseRifleJob());
            item.Add(data);
            item.Add(data.CommonData);
            item.Add(data.ScatteringData);
            
            item.Item.Job.Run(item);
            
            return item;
        }
    }
}