namespace Yogurt.Arena;

[Serializable] 
public class Observable<T>
{
    public T Value
    {
        get => value;
        set => Set(value);
    }

    public Event<T> ChangedEvent { get; } = new Event<T>();
        
    private T value;

    public Observable(T value = default)
    {
        this.value = value;
    }
        
    public void Set(T newValue)
    {
        if (value != null && value.Equals(newValue)) return;

        value = newValue;
        ChangedEvent.Fire(value);
    }

    public void Listen(Action<T> action)
    {
        ChangedEvent.Listen(action);
    }

    public void Unsubscribe(object owner)
    {
        ChangedEvent.Unsubscribe(owner);
    }

    public static implicit operator T(Observable<T> observable)
    {
        return observable.value;
    }

    public bool Equals(Observable<T> other)
    {
        return other.value.Equals(value);
    }

    public override bool Equals(object other)
    {
        return other is Observable<T> observable && observable.value.Equals(value);
    }

    public override int GetHashCode()
    {
        return value.GetHashCode();
    }

    public override string ToString()
    {
        return value.ToString();
    }
}