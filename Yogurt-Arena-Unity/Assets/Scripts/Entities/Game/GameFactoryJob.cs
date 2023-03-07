using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Assets assets = new Assets();
            Data data = await assets.Data.Load();

            Entity.Create()
                .Add<Game>()
                .Add(assets)
                .Add(data);
        }
    }
}