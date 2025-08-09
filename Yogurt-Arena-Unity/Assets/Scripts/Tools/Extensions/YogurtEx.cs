using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class EntityEx
    {
        public static Entity Link(this Entity entity, GameObject go)
        {
            if (!go.TryGetComponent(out EntityLink link))
            {
                link = go.AddComponent<EntityLink>();
            }
            
            link.Set(entity);
            return entity;
        }

        public static Lifetime Life(this Entity entity)
        {
            Lifetime life = new();
            KillWithEntity();
            return life;

            async void KillWithEntity() => Wait.While(EntityExist).ContinueWith(life.Kill);
            bool EntityExist() => entity.Exist;
        }

        public static Lifetime Life(this IAspect aspect)
        {
            return aspect.Entity.Life();
        }
        
        public static Entity Add(this Entity entity, IEnumerable<IComponent> components)
        {
            foreach (IComponent component in components)
            {
                entity.Add(component);
            }

            return entity;
        }

        public static async UniTaskVoid Run(this Entity entity, Action action)
        {
            while (entity.Exist)
            {
                action();
                await Wait.Update();
            }
        }
        
        public static async UniTaskVoid Run(this Entity entity, Func<UniTask> action)
        {
            while (entity.Exist)
            {
                await action();
                await Wait.Update();
            }
        }

        public static async UniTaskVoid Run(this IAspect aspect, Action action)
        {
            while (aspect.Exist())
            {
                action();
                await Wait.Update();
            }
        }
        
        public static async UniTaskVoid Run(this IAspect aspect, Func<UniTask> action)
        {
            while (aspect.Exist())
            {
                await action();
                await Wait.Update();
            }
        }
    }
}