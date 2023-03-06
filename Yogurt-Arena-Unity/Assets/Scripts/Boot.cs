using UnityEngine;

namespace Yogurt.Arena
{
    public class Boot : MonoBehaviour
    {
        private void Awake()
        {
            new InitializeGameJob().Run();
            new RunWorldJob().Run();
        }
    }
}