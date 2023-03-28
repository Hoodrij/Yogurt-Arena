using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Yogurt.Arena
{
    public struct KillBulletJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            await UniTask.Delay(0.05f.Seconds());
            bullet.View.transform.DOScale(0, 0.1f);

            await UniTask.Delay(0.3f.Seconds());
            bullet.Kill();
        }
    }
}