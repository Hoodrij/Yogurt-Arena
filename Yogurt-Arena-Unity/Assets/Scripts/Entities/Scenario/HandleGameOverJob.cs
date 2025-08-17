namespace Yogurt.Arena
{
    public struct HandleGameOverJob
    {
        public async UniTask Run()
        {
            await Wait.Seconds(0.5f);
            
            GameOverWidget gameOverWidget = Query.Single<UIView>().GameOverWidget;
            gameOverWidget.Show();

            await gameOverWidget.OnRestartClick;
            
            Query.Of<World>().Single().Kill();
            await Wait.Seconds(0.1f);
        }
    }
}