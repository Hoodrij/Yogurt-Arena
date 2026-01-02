namespace Yogurt.Arena;

public struct RunGameLoopJob
{
    public void Run()
    {
        Entity game = Query.Of<Game>().Single();
        game.Run(Loop);
        return;


        async UniTask Loop()
        {
            await new WorldFactoryJob().Run();
            new RunScenarioJob().Run().Forget();
                
            await new WaitForGameOverJob().Run();
            await new HandleGameOverJob().Run();
        }
    }
}