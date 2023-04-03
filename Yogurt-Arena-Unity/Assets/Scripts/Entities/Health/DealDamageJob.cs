namespace Yogurt.Arena
{
    public struct DealDamageJob
    {
        public void Run(Entity target, int damage)
        {
            if (target.TryGet(out Health health))
            {
                health.Value -= damage;
                if (health.Value <= 0)
                {
                    target.Kill();
                }
            }
        }
    }
}