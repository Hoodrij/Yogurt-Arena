using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class EntityEx
    {
        public static Entity AddLink(this Entity entity, GameObject go)
        {
            if (!go.TryGetComponent(out EntityLink link))
            {
                link = go.AddComponent<EntityLink>();
            }
            
            entity.Add(link);
            link.Set(entity);
            entity.WaitForDeadAndDispose(link);

            return entity;
        }

        public static async UniTask WaitForDead(this Entity entity)
        {
            await UniTask.WaitWhile(() => Application.isPlaying && entity.Exist);
        }
        
        private static async void WaitForDeadAndDispose<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            await entity.WaitForDead();

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