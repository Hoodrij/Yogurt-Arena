namespace Yogurt.Arena
{
    public struct ChangeHealthJob
    {
        public interface IHealthChangedJob
        {
            void Run(Health health);
        }
        
        public void Run(Entity target, int delta)
        {
            if (!target.Exist) return;
            
            if (target.TryGet(out Health health))
            {
                health.Value += delta;
                health.Job?.Run(health);
                if (health.Value <= 0)
                {
                    target.Kill();
                }
            }
        }
    }
}