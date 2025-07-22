using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForGameOverJob
    {
        public async UniTask Run()
        {
            await Query.Single<PlayerAspect>()
                .Life();
        }
    }
}