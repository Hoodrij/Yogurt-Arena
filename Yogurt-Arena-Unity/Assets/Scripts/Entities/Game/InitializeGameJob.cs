namespace Yogurt.Arena
{
    public struct InitializeGameJob
    {
        public void Run()
        {
            Entity.Create()
                .Add<Game>()
                .Add<Assets>();
            
            
        }
    }
}