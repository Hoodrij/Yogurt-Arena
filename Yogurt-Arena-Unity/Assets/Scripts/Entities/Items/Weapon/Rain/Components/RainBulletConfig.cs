﻿using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class RainBulletConfig : IComponent
    {
        public ExplosionConfig Explosion;
        public Vector3 Gravity;
        public float FindTargetDistance;
        public float BulletRotationSpeed;
        public float BulletSpeedChangeCoef;
    }
}