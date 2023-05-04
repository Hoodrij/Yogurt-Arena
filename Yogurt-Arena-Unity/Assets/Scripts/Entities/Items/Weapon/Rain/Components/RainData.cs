﻿using System;
using UnityEngine;

namespace Yogurt.Arena
{
    [Serializable]
    public class RainData : IComponent
    {
        public WeaponData CommonData;
        public WeaponClipData ClipData;
        public Vector3 Gravity;
        public float FindTargetDistance;
        public float ChaseValue1;
        public float ChaseValue2;
    }
}