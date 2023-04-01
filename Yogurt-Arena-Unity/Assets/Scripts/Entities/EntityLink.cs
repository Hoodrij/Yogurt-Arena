using System;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class EntityLink : MonoBehaviour, IComponent, IDisposable
    {
        public Entity Entity { get; private set; }

        public void Set(Entity entity)
        {
            Entity = entity;
            entity.Add(this);
        }
        
        public static implicit operator Entity(EntityLink link) => link.Entity;

        public void Dispose()
        {
            if (TryGetComponent(out PoolLink poolLink))
            {
                poolLink.Release();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}