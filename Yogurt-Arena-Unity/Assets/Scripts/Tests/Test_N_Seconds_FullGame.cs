using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

namespace Yogurt.Arena
{
    public class Test_N_Seconds_FullGame
    {
        [OneTimeTearDown]
        public void TearDownOnce() => ProfileResult.Start();

        [Test]
        public async Task Run()
        {
            await new GameFactoryJob().Run();
            new RunGameLoopJob().Run();

            await UniTask.Delay(30.ToSeconds());
        } 
    }
}