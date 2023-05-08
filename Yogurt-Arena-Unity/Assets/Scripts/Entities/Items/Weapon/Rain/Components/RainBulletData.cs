using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class RainBulletData : IComponent
    {
        public AoeDamage Damage;
        public Vector3 Gravity;
        public float FindTargetDistance;
        public float BulletRotationSpeed;
        public float BulletSpeedChangeCoef;
        public PooledAsset<ExplosionView> ExplosionAsset;
    }
}