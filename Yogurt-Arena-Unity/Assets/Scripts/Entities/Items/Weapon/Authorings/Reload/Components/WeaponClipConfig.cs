using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class WeaponClipConfig : IComponent
    {
        public int BulletsInClip;
        public float ClipCooldown;
    }
}