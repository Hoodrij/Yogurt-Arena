using UnityEngine;

namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            Destroy(gameObject);
            await new GameFactoryJob().Run();
            await new WorldFactoryJob().Run();
            new RunScenarioJob().Run();
        }
    }
}