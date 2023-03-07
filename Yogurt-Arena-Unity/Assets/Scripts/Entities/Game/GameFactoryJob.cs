namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public void Run()
        {
            Entity.Create()
                .Add<Game>()
                .Add<Assets>();
        }
    }
}