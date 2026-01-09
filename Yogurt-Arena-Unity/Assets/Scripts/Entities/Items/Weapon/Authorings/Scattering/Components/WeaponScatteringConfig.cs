namespace Yogurt.Arena;

[Serializable]
public record struct WeaponScatteringConfig() : IComponent
{
    public MinMax VelocityMagnitudeModifier = new MinMax(1, 1);
    public float Angle;
}