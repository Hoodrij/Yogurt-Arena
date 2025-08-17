namespace Yogurt.Arena.Tools;

public class PoolLink : MonoBehaviour, IDisposable
{
    internal Pool Pool;

    public void Release()
    {
        Pool.Push(gameObject);
    }

    public void Dispose()
    {
        Release();
    }
}