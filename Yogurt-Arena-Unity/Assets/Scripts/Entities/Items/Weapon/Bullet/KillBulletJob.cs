using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct KillBulletJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            bullet.Add<Kinematic>();
            bullet.Body.Velocity = Vector3.zero;

            bullet.View.transform.DOScale(2, 0.1f);
            await UniTask.Delay(0.1f.ToSeconds());
            bullet.View.transform.DOScale(0, 0.3f);

            await UniTask.Delay(0.3f.ToSeconds());
            bullet.Kill();
        }
    }
}