using System;

namespace Yogurt.Arena
{
    public class Observable<T> where T : IEquatable<T>
    {
        public Event<T> OnChanged = new();
        
        public Observable(Func<T> getter, Entity lifetime = default)
        {
            T currentValue = getter.Invoke();
            
            if (!lifetime.Exist)
            {
                lifetime = Query.Of<Game>().Single();
            }
            lifetime.Run(Update);
            return;


            void Update()
            {
                T newValue = getter.Invoke();
                if (!currentValue.Equals(newValue))
                {
                    OnChanged.Fire(newValue);
                    currentValue = newValue;
                }
            }
        }
    }
}