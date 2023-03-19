using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class RifleFactoryJob
    {
        public async UniTask<ItemAspect> Run()
        {
            return World.Create()
                .Add(new Item
                {
                    Job = new UseRifleJob()
                })
                .As<ItemAspect>();
        }
    }
}