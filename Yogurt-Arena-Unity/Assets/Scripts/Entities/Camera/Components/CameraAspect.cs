namespace Yogurt.Arena;

public record struct CameraAspect(Entity Entity) : IAspect
{
    public CameraConfig Config => this.Get<CameraConfig>();

    public CameraView View => this.Get<CameraView>();
        
    public Camera Camera => View.Camera;
}