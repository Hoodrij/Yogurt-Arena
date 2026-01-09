namespace Yogurt.Arena;

[Serializable]
public record struct BulletConfig() : IComponent
{
    public PooledAsset<BulletView> Asset = new();
    public LayerMask CollisionMask;
    public float Radius;
    public int Damage;
    public float LifeTime;
    public float Speed;
}