namespace Yogurt.Arena;

[Serializable]
public class BeaconConfig : ScriptableObject, IComponent, IConfigSO
{
    public Asset<BeaconView> Asset;
    public float SmoothValue;
    public float Elasticity;

    [Header("Beacon Animation")]
    [Min(0f)] public float DisappearDuration = 0.12f;
    [Min(0f)] public float AppearDuration = 0.15f;
        
    public IEnumerable<IComponent> GetComponents()
    {
        yield return this;
    }
}