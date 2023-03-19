using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class BulletView : MonoBehaviour, IComponent, IDisposable
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}