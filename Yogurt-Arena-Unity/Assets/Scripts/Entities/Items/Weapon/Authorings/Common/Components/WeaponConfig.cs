namespace Yogurt.Arena;

[Serializable]
public record struct WeaponConfig() : IComponent
{
    public BulletConfig Bullet = new();
    public float Cooldown;
    public float Range;
    public float AngleToAttack;
}