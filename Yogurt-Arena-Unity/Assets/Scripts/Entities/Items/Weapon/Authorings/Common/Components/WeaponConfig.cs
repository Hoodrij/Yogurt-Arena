using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class WeaponConfig : IComponent
    {
        public BulletConfig Bullet;
        public float Cooldown;
        public float Range;
        public float AngleToAttack;
    }
}