using System;
using UnityEngine;

namespace Yogurt.Arena
{
    internal class WeakAction<T>
    {
        public bool IsAlive => owner.IsAlive && owner?.Target != null && IsAliveAsMonoBeh();
        
        private readonly WeakReference owner;
        private readonly Action<T> callback;

        public WeakAction(Action<T> action)
        {
            owner = new WeakReference(action.Target);
            callback = action;
        }

        public void Invoke(T t)
        {
            if (IsActiveAsMonoBeh())
                callback.Invoke(t);
        }

        public bool IsOwnedBy(object owner) => this.owner.Target == owner;
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
    
    internal class WeakAction
    {
        public bool IsAlive => owner.IsAlive && owner?.Target != null && IsAliveAsMonoBeh();
        
        private readonly WeakReference owner;
        private readonly Action callback;

        public WeakAction(Action action)
        {
            owner = new WeakReference(action.Target);
            callback = action;
        }

        public void Invoke()
        {
            if (IsActiveAsMonoBeh())
                callback.Invoke();
        }

        public bool IsOwnedBy(object owner) => this.owner.Target == owner;
        
        private bool IsAliveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono != null && mono.gameObject != null;
        private bool IsActiveAsMonoBeh() => !(owner.Target is MonoBehaviour mono) || mono.gameObject.activeInHierarchy;
    }
}