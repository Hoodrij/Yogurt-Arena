namespace Yogurt.Arena;

[System.Serializable]
public record struct AoeDamage : IComponent
{
    public int Damage;
    public float Radius;
    public LayerMask HitMask;
}