namespace Yogurt.Arena;

[Serializable]
public record struct ExplosionConfig : IComponent
{
    public PooledAsset<ExplosionView> Asset;
    public AoeDamage Damage;
}