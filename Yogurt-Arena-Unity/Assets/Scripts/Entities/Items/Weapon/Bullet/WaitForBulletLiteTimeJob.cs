using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct WaitForBulletLiteTimeJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            await UniTask.Delay(bullet.Data.LifeTime.ToSeconds(), DelayType.Realtime);
        }   
    }
}