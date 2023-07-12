namespace Yogurt.Arena
{
    public struct DealDamageJob
    {
        public void Run(Entity target, int damage)
        {
            new ChangeHealthJob().Run(target, -damage);
        }
    }
}