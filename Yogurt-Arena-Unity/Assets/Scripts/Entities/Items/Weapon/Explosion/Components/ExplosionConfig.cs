namespace Yogurt.Arena;

[Serializable]
public class ExplosionConfig : IComponent
{
    public PooledAsset<ExplosionView> Asset;
    public AoeDamage Damage;
}