using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct LevelFactoryJob
    {
        public async UniTask Run()
        {
            IAsset<Level> level = Query.Single<Assets>().Level;
            
            Entity entity = World.Create()
                .Add(await level.Spawn());
        }
    }
}