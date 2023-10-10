using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunGameLoopJob
    {
        public async void Run()
        {
            Entity game = Query.Of<Game>().Single();
            game.Run(Loop);
            return;


            async UniTask Loop()
            {
                await new WorldFactoryJob().Run();
                new RunScenarioJob().Run();
                
                await new WaitForGameOverJob().Run();
                await new HandleGameOverJob().Run();
            }
        }
    }
}