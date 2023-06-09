using System.Linq;
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
        }
    }
}