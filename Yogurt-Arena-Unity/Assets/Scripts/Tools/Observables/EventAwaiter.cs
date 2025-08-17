using System.Runtime.CompilerServices;
using Event = Yogurt.Arena.Event;

public static class EventEx
{
    public static EventAwaiter GetAwaiter(this Event e) => new EventAwaiter(e);
    public static EventAwaiter<T> GetAwaiter<T>(this Event<T> e) => new EventAwaiter<T>(e);
}

namespace Yogurt.Arena
{
    public class EventAwaiter : INotifyCompletion
    {
        private readonly Event observable;
        private bool isCompleted;
        private Action continuation;

        internal EventAwaiter(Event observable)
        {
            this.observable = observable;
        }

        public bool IsCompleted => isCompleted;
        public void GetResult() { }

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            observable.Listen(Listener);
        }

        private void Listener()
        {
            isCompleted = true;
            continuation();
        }
    }
    
    public class EventAwaiter<T> : INotifyCompletion
    {
        private readonly Event<T> observable;
        private bool isCompleted;
        private Action continuation;
        private T result;

        internal EventAwaiter(Event<T> observable)
        {
            this.observable = observable;
        }

        public bool IsCompleted => isCompleted;
        public T GetResult() => result;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            observable.Listen(Listener);
        }

        private void Listener(T result)
        {
            isCompleted = true;
            this.result = result;
            continuation();
        }
    }
}