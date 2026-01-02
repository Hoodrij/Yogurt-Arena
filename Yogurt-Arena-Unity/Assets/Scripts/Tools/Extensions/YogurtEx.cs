namespace Yogurt.Arena;

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

    private static Dictionary<Entity, Lifetime> lifes = new();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void EnterPlayMode()
    {
        foreach (Lifetime life in lifes.Values)
        {
            life.Kill();
        }
        lifes.Clear();
    }

    public static Lifetime Life(this Entity entity)
    {
        if (lifes.TryGetValue(entity, out Lifetime life))
        {
            return life;
        }
        
        life = new();
        lifes.Add(entity, life);
        KillWithEntity(entity, life).Forget();
        return life;
        
        static async UniTaskVoid KillWithEntity(Entity entity, Lifetime life)
        {
            await Wait.While(static e => e.Exist, state: entity);
            life.Kill();
            lifes.Remove(entity);
        }
    }
    
    public static Lifetime Life<TAspect>(this TAspect aspect) where TAspect : IAspect
    {
        return aspect.Entity.Life();
    }

    public static Entity PopulateFrom(this Entity entity, EntityBlueprint blueprint)
    {
        blueprint.Blueprint.Populate(entity);
        return entity;
    }

    public static void Run(this Entity entity, Action action)
    {
        Loop().Forget();
        return;

        async UniTask Loop()
        {
            while (entity.Exist)
            {
                action();
                await Wait.Update();
            }
        }
    }
        
    public static void Run(this Entity entity, Func<UniTask> action)
    {
        Loop().Forget();
        return;

        async UniTask Loop()
        {
            while (entity.Exist)
            {
                await action();
                await Wait.Update();
            }
        }
    }

    public static void Run<TAspect>(this TAspect aspect, Action action) where TAspect : struct, IAspect
    {
        Loop().Forget();
        return;

        async UniTask Loop()
        {
            while (aspect.Entity.Exist)
            {
                action();
                await Wait.Update();
            }
        }
    }
        
    public static void Run<TAspect>(this TAspect aspect, Func<UniTask> action) where TAspect : struct, IAspect
    {
        Loop().Forget();
        return;

        async UniTask Loop()
        {
            while (aspect.Entity.Exist)
            {
                await action();
                await Wait.Update();
            }
        }
    }
}