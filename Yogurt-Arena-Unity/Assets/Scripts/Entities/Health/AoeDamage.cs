using UnityEngine;

namespace Yogurt.Arena
{
    [System.Serializable]
    public class AoeDamage : IComponent
    {
        public int Damage;
        public float Radius;
        public LayerMask HitMask;
    }
}