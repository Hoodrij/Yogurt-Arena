﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public struct SpawnExplosionJob
    {
        public async UniTask Run(PooledAsset<ExplosionView> asset, Vector3 position, float radius)
        {
            ExplosionView vfx = await asset.Spawn();
            Transform view = vfx.View;
            float duration = 0.1f;
            
            vfx.transform.position = position;
            view.localScale = Vector3.zero;
            view.DOScale(radius, duration);
            await UniTask.Delay(duration.ToSeconds());
            
            view.DOScale(0, duration).SetEase(Ease.InSine);
            await UniTask.Delay(duration.ToSeconds());

            view.DOKill();
            vfx.GetComponent<PoolLink>().Release();
        }
    }
}