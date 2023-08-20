using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class WeaponConfig : IComponent
    {
        public BulletConfig Bullet = new();
        public float Cooldown;
        public float Range;
        public float AngleToAttack;
    }
}