using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Application.targetFrameRate = 90;
            
            Assets assets = new Assets();
            Data data = await assets.Data.Load();

            Entity.Create()
                .Add<Game>()
                .Add(assets)
                .Add(data);
        }
    }
}