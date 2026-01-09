namespace Yogurt.Arena;

public record struct Time : IComponent
{
    public const int TARGET_FRAME_RATE = 90;

    public float ExpectedDelta;
    public float Delta;

    public float Scale;
        
    public Time()
    {
        QualitySettings.maxQueuedFrames = 2;
        Application.targetFrameRate = TARGET_FRAME_RATE;
            
        int expectedFps = TARGET_FRAME_RATE;
        ExpectedDelta = 1f / expectedFps;
    }
        
    public static implicit operator float(Time time)
    {
        return time.Scale;
    }
}