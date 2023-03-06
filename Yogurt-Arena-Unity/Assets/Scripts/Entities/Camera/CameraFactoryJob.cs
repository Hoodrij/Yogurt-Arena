namespace Yogurt.Arena
{
    public struct CameraFactoryJob
    {
        public void Run(CameraView view)
        {
            Entity.Create()
                .Add(new Camera
                {
                    View = view
                });
        }
    }
}