using System;
using System.Collections.Generic;
using System.Threading;
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
            
            link.Set(entity);
            entity.WaitForDeadAndDispose(link);

            return entity;
        }

        public static async UniTask WaitForDead(this Entity entity)
        {
            await Wait.While(() => entity.Exist);
        }
        
        private static async UniTask WaitForDeadAndDispose<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            await entity.WaitForDead();

            if (Application.isPlaying)
            {
                component.Dispose();
            }
        }
        
        public static CancellationToken Lifetime(this Entity entity)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            WaitForDeadAndCancelCts();
            return cts.Token;

            async UniTaskVoid WaitForDeadAndCancelCts()
            {
                await entity.WaitForDead();
                cts.Cancel();
            }
        }

        public static Entity Add(this Entity entity, IEnumerable<IComponent> components)
        {
            foreach (IComponent component in components)
            {
                entity.Add(component);
            }

            return entity;
        }

        public static async UniTask Run(this Entity entity, Action action)
        {
            while (entity.Exist)
            {
                action();
                await Wait.Update(entity);
            }
        }
        
        public static async UniTask Run(this Entity entity, Func<UniTask> action)
        {
            while (entity.Exist)
            {
                await action();
                await Wait.Update(entity);
            }
        }

        public static async UniTask Run(this IAspect aspect, Action action)
        {
            while (aspect.Exist())
            {
                action();
                await Wait.Update(aspect.Entity);
            }
        }
        
        public static async UniTask Run(this IAspect aspect, Func<UniTask> action)
        {
            while (aspect.Exist())
            {
                await action();
                await Wait.Update(aspect.Entity);
            }
        }
    }
}