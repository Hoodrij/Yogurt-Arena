﻿using UnityEngine;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class RainBulletData : IComponent
    {
        public Vector3 Gravity;
        public float FindTargetDistance;
        public float BulletRotationSpeed;
        public float BulletSpeedChangeCoef;
    }
}