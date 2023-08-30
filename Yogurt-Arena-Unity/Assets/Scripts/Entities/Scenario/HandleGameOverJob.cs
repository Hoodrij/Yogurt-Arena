using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct HandleGameOverJob
    {
        public async UniTaskVoid Run()
        {
            await new WaitForGameOverJob().Run();
            await Wait.Seconds(0.5f);

            GameOverWidget gameOverWidget = Query.Single<UIView>().GameOverWidget;
            gameOverWidget.Show();

            await gameOverWidget.OnRestartClick;
            
            Query.Of<World>().Single().Kill();
            
            await new WorldFactoryJob().Run();
            new RunScenarioJob().Run();
        }
    }
}