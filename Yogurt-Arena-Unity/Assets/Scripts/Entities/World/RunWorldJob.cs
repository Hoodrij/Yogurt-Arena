using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RunWorldJob
    {
        public async UniTask Run()
        {
            new WorldFactoryJob().Run();
            
            
        }
    }
}