using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    public struct SpawnExplosionJob
    {
        public async UniTask Run(ExplosionConfig config, Vector3 position)
        {
            ExplosionView vfx = await config.Asset.Spawn();
            Transform view = vfx.View;
            float duration = 0.15f;
            float halfDuration = 0.15f;
            
            vfx.transform.position = position;
            view.localScale = Vector3.zero;
            view.DOScale(config.Damage.Radius * 2, halfDuration);
            await Wait.Seconds(halfDuration);
            
            view.DOScale(0, duration).SetEase(Ease.OutSine);
            await Wait.Seconds(duration);

            view.DOKill();
            vfx.GetComponent<PoolLink>().Release();
        }
    }
}