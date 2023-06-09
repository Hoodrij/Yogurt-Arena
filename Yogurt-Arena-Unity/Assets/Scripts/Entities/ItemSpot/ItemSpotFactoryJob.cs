﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct ItemSpotFactoryJob
    {
        public async UniTask<ItemSpotAspect> Run(ItemSpotView view)
        {
            ItemSpotData data = Query.Single<Data>().ItemSpot;
            
            Entity entity = World.Create();
            entity.Add(new BodyState
                {
                    Position = view.transform.position
                })
                .Add(new ItemSpotState
                {
                    Type = EItemType.Empty,
                    Radius = data.Radius,
                    Mask = data.Mask
                })
                .Add(view);

            return entity.As<ItemSpotAspect>();
        }
    }
}