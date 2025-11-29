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

    public static Lifetime Life(this Entity entity)
    {
        Lifetime life = new();
        KillWithEntity();
        return life;

        async void KillWithEntity() => Wait.While(EntityExist).ContinueWith(life.Kill).Forget();
        bool EntityExist() => entity.Exist;
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