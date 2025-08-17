namespace Yogurt.Arena
{
    public struct CameraAspect : IAspect
    {
        public Entity Entity { get; set; }

        public CameraConfig Config => this.Get<CameraConfig>();

        public CameraView View => this.Get<CameraView>();
        
        public Camera Camera => View.Camera;
    }
}