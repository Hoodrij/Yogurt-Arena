namespace Yogurt.Arena
{
    [System.Serializable]
    public class ChargeConfig : IComponent
    {
        public WeaponConfig Common;
        public ItemLifetimeConfig Lifetime;
        public TargetDetectionConfig TargetDetection;
    }
}