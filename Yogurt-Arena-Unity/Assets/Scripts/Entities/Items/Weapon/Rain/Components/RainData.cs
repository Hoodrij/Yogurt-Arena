using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class RainData : IComponent
    {
        public WeaponData CommonData;
        public WeaponClipData ClipData;
    }
}