namespace Yogurt.Arena;

public record struct Health : IComponent
{
    public int MaxHealth;
    public int Value;
    public HealthWidget HealthWidget;
    public IDeathJob DeathJob;
}

public interface IDeathJob
{
    UniTaskVoid Run(Entity entity);
}