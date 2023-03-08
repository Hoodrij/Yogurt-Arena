using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraAspect : IAspect
    {
        public Entity Entity { get; set; }

        public CameraView View => this.Get<CameraView>();
        
        public Camera Camera => View.Camera;
        public Transform Transform => View.transform;
    }
}