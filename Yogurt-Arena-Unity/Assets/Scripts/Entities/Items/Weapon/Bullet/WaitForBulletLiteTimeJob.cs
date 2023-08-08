using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForBulletLiteTimeJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            await Wait.Seconds(bullet.Config.LifeTime);
        }   
    }
}