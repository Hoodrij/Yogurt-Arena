using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class CameraView : MonoBehaviour, IComponent, IDisposable
    {
        public Camera Camera;
        public Transform Transform;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}