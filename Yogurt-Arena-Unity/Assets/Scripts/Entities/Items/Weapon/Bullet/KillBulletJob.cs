using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Yogurt.Arena
{
    public struct KillBulletJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            bullet.Add<Kinematic>();
            await UniTask.Delay(0.05f.ToSeconds());
            bullet.View.transform.DOScale(0, 0.1f);

            await UniTask.Delay(0.3f.ToSeconds());
            bullet.Kill();
        }
    }
}