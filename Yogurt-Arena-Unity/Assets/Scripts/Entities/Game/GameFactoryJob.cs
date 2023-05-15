using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Data data = Resources.Load<Data>("Data");

            Entity.Create()
                .Add<Game>()
                .Add(data)
                .Add<EthernalLifetime>()
                .Add<Time>();
        }
    }
}