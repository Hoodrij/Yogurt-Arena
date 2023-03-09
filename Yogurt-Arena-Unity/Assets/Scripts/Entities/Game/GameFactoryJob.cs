using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Application.targetFrameRate = 1000;
            
            Assets assets = new Assets();
            Data data = await assets.Data.Load();

            Entity.Create()
                .Add<Game>()
                .Add(assets)
                .Add(data);
        }
    }
}