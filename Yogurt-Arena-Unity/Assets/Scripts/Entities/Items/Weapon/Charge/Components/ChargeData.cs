using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class ChargeData : IComponent
    {
        public BulletData Bullet;
        public float Cooldown;
        public float Range;
        public float AngleToAttack;
    }
}