using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class RifleData : IComponent
    {
        public BulletData Bullet;
        public float Cooldown;
    }
}