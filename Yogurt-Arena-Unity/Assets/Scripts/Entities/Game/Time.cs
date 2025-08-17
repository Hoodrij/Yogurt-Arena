namespace Yogurt.Arena
{
    public class Time : IComponent
    {
        public readonly int TARGET_FRAME_RATE = 90;
        
        public float ExpectedDelta;
        public float Delta;

        public float Scale;
        
        public Time()
        {
            QualitySettings.maxQueuedFrames = 2;
            Application.targetFrameRate = TARGET_FRAME_RATE;
            
            int expectedFps = TARGET_FRAME_RATE;
            ExpectedDelta = 1f / expectedFps;

            UpdateScale().Forget();
        }

        private async UniTask UpdateScale()
        {
            while (Application.isPlaying)
            {
                Delta = UnityEngine.Time.deltaTime;
                Scale = Delta / ExpectedDelta;
                await Wait.Update();
            }
        }
        
        public static implicit operator float(Time time)
        {
            return time.Scale;
        }
    }
}