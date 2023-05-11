using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Application.targetFrameRate = 90;

            Data data = Resources.Load<Data>("Data");

            Entity.Create()
                .Add<Game>()
                .Add(data)
                .Add<EthernalLifetime>();
        }
    }
}