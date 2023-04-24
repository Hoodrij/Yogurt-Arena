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

            float t = 0.1f;
            bullet.View.transform.DOScale(2, t);
            await UniTask.Delay(t.ToSeconds());
            bullet.View.transform.DOScale(0, t);

            await UniTask.Delay(0.3f.ToSeconds());
            bullet.Kill();
        }
    }
}