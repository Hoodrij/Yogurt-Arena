using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class ExplosionConfig : IComponent
    {
        public PooledAsset<ExplosionView> Asset;
        public AoeDamage Damage;
    }
}