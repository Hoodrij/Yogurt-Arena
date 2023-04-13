using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class ChargeData : IComponent
    {
        public float Duration;
        public float Cooldown;
    }
}