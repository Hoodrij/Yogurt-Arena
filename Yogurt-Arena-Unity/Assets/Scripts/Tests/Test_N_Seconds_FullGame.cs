using NUnit.Framework;

namespace Yogurt.Arena
{
    public class Test_N_Seconds_FullGame
    {
        [Test]
        public async Task Run()
        {
            await new GameFactoryJob().Run();
            new RunGameLoopJob().Run();

            await UniTask.Delay(30.ToSeconds());
        } 
    }
}