using UnityEngine;
using Yogurt.Arena.Components;

namespace Yogurt.Arena
{
    public struct UIFactoryJob
    {
        public async Awaitable<Entity> Run()
        {
            UIData data = Query.Single<Data>().UIData;
            UIView uiView = await data.Asset.Spawn();

            Entity entity = World.Create()
                .AddLink(uiView.gameObject)
                .Add(uiView)
                .Add(data);

            return entity;
        }
    }
}