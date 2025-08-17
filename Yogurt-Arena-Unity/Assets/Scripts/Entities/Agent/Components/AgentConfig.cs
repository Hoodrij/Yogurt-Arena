namespace Yogurt.Arena;

public class AgentConfig : ScriptableObject, IComponent, IConfigSO
{
    public PooledAsset<AgentView> Asset;
    public PooledAsset<AgentDeathVFX> DeathVFX;
    public AgentType Type;
    public TeamType Team;
    public ItemType Weapon;
    public float MoveSpeed;
    public float SlowDistance;
    public float MoveSmoothValue;
    public float LookSmoothValue;
    public int MaxHealth;
    public int Health;

    public IEnumerable<IComponent> GetComponents()
    {
        yield return this;
    }
}