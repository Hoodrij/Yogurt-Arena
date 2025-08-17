namespace Yogurt.Arena;

public struct ChangeHealthJob
{
    public async void Run(Entity target, int delta)
    {
        if (!target.Exist) return;
            
        if (target.TryGet(out Health health))
        {
            health.Value += delta;
            health.Value.Clamp(0, health.MaxHealth);
            new UpdateHealthWidgetJob().Run(health);
                
            if (health.Value <= 0)
            {
                health.DeathJob.Run(target);
                target.Kill();
            }
        }
    }
}