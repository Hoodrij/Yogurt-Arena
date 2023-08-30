namespace Yogurt.Arena
{
    public struct HandleGameOverJob
    {
        public async void Run()
        {
            // await new WaitForGameOverJob().Run();
            await Wait.Seconds(0.5f);

            GameOverWidget gameOverWidget = Query.Single<UIView>().GameOverWidget;
            gameOverWidget.Show();

            0.log();
            await gameOverWidget.OnRestartClick;
            1.log();
            
            await Wait.Seconds(0.5f);
            Query.Of<World>().Single().Kill();
            2.log();
            
            await Wait.Seconds(0.5f);
            await new WorldFactoryJob().Run();
            3.log();
            await Wait.Seconds(0.5f);
            new RunScenarioJob().Run();
        }
    }
}