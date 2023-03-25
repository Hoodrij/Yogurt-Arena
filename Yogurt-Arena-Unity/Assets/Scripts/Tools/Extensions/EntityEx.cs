using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class EntityEx
    {
        public static Entity AddLink(this Entity entity, GameObject go)
        {
            EntityLink link = go.AddComponent<EntityLink>();
            entity.Add(link);
            link.Set(entity);
            entity.WaitForDeadAndDispose(link);

            return entity;
        }
        
        private static async void WaitForDeadAndDispose<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            await UniTask.WaitWhile(() => Application.isPlaying && entity.Exist);

            if (Application.isPlaying)
            {
                component.Dispose();
            }
        }

        public static async void Run(this Entity entity, IUpdateJob job)
        {
            while (Application.isPlaying && entity.Exist)
            {
                job.Update();
                await UniTask.Yield();
            }
        }
    }
}