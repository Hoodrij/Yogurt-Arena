using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class BulletView : MonoBehaviour, IComponent, IDisposable
    {
        public Rigidbody Body;
        
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}