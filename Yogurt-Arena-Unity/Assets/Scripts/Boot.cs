using UnityEngine;

namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            new InitializeGameJob().Run();
            await new RunWorldJob().Run();
        }
    }
}