namespace Yogurt.Arena
{
    public struct HealJob
    {
        public void Run(Entity target, int heal)
        {
            Health health = target.Get<Health>();
            health.Value += heal;
            UnityEngine.Debug.Log(health.Value);
        }
    }
}