using UnityEngine;

namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            Destroy(gameObject);
            await new GameFactoryJob().Run();
            await new RunWorldJob().Run();

            123.log();
            // Awaitable task = test();
            // task.Cancel();
            // await Wait.Any(test(), test2());
            456.log();
        }
        
        async Awaitable test2()
        {
            await Awaitable.WaitForSecondsAsync(2);
            2.log();
        }

        async Awaitable test()
        {
            await Awaitable.WaitForSecondsAsync(1);
            1.log();
        }
    }
}