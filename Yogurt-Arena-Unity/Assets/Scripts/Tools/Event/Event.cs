using System;

namespace Yogurt.Arena
{
    public class Event
    {
        private event Action OnWhatever;

        public void Listen(Action action) => OnWhatever += action;
        public void Fire()
        {
            OnWhatever?.Invoke();
            OnWhatever = null;
        }
    }
    
    public class Event<T>
    {
        private event Action<T> OnWhatever;

        public void Listen(Action<T> action) => OnWhatever += action;
        public void Fire(T t)
        {
            OnWhatever?.Invoke(t);
            OnWhatever = null;
        }
    }
}