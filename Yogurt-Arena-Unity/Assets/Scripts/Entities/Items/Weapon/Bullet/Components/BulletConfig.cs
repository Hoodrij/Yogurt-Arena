using System;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class BulletConfig : IComponent
    {
        public PooledAsset<BulletView> Asset = new();
        public LayerMask CollisionMask;
        public float Radius;
        public int Damage;
        public float LifeTime;
        public float Speed;
    }
}