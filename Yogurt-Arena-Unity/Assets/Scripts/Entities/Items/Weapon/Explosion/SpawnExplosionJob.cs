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
            float duration = 0.1f;
            
            vfx.transform.position = position;
            view.localScale = Vector3.zero;
            view.DOScale(config.Damage.Radius * 2, duration);
            await Wait.Seconds(duration);
            
            view.DOScale(0, duration).SetEase(Ease.InSine);
            await Wait.Seconds(duration);

            view.DOKill();
            vfx.GetComponent<PoolLink>().Release();
        }
    }
}