using UnityEngine;

namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            await new GameFactoryJob().Run();
            await new RunWorldJob().Run();
        }
    }
}