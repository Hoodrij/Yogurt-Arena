using System;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class BulletData : IComponent
    {
        public PooledAsset<BulletView> Asset;
        public LayerMask HitMask;
        public float Radius;
        public int Damage;
        public float LifeTime;
        public float Speed;
    }
}