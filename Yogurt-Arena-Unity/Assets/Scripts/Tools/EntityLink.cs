namespace Yogurt.Arena;

public class EntityLink : MonoBehaviour, IComponent, IDisposable
{
    public Entity Entity { get; private set; }
        
    public void Set(Entity entity)
    {
        Entity = entity;
        Entity.Add(this);
        WaitForDeadAndDispose().Forget();
        return;
            
        async UniTask WaitForDeadAndDispose()
        {
            await Entity.Life();
            // we Clear Entity in case if GO want to live longer
            if (Entity != Entity.Null)
                Dispose();
        }
    }
        
    public void Clear()
    {
        Entity.Remove<EntityLink>();
        Entity = Entity.Null;
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