namespace Yogurt.Arena;

[System.Serializable]
public record struct RainBulletConfig : IComponent
{
    public ExplosionConfig Explosion;
    public Vector3 Gravity;
    public float FindTargetDistance;
    public float BulletRotationSpeed;
    public float BulletSpeedChangeCoef;
}