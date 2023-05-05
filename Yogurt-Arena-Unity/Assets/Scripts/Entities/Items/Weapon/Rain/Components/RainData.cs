using System;
using UnityEngine;

namespace Yogurt.Arena
{
    [Serializable]
    public class RainData : IComponent
    {
        public WeaponData CommonData;
        public WeaponClipData ClipData;
        public RainBulletData BulletData;
    }
}