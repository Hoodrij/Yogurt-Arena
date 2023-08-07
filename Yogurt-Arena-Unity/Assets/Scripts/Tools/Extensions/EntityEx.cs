using System;
using System.Collections.Generic;
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

        public static async Awaitable WaitForDead(this Entity entity)
        {
            await Wait.While(() => entity.Exist);
        }
        
        private static async Awaitable WaitForDeadAndDispose<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            await entity.WaitForDead();

            if (Application.isPlaying)
            {
                component.Dispose();
            }
        }

        public static async Awaitable Run(this Entity entity, Action action)
        {
            while (entity.Exist)
            {
                action();
                await Wait.Update();
            }
        }
        
        public static async Awaitable Run(this Entity entity, Func<Awaitable> action)
        {
            while (entity.Exist)
            {
                await action();
                await Wait.Update();
            }
        }

        public static async Awaitable Run(this IAspect aspect, Action action)
        {
            while (aspect.Exist())
            {
                action();
                await Wait.Update();
            }
        }
        
        public static async Awaitable Run(this IAspect aspect, Func<Awaitable> action)
        {
            while (aspect.Exist())
            {
                await action();
                await Wait.Update();
            }
        }
    }
}