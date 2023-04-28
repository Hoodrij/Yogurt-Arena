using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class WeaponClipData : IComponent
    {
        public int BulletsInClip;
        public float ClipCooldown;
    }
}