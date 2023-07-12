namespace Yogurt.Arena
{
    public class Health : IComponent
    {
        public int MaxHealth;
        public int Value;
        public ChangeHealthJob.IHealthChangedJob Job;
    }
}