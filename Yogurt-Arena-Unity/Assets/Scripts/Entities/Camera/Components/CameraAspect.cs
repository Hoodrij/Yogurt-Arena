using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraAspect : IAspect
    {
        public Entity Entity { get; set; }

        public CameraData Data => this.Get<CameraData>();

        public CameraView View => this.Get<CameraView>();
        
        public Camera Camera => View.Camera;
    }
}