﻿using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct KillBulletJob
    {
        public async UniTask Run(BulletAspect bullet)
        {
            bullet.Add(new Kinematic());
            bullet.Body.Velocity = Vector3.zero;

            float t = 0.1f;

            bullet.View.transform.DOScale(2, t);
            await Wait.Seconds(t);
            bullet.View.transform.DOScale(0, t);
            await Wait.Seconds(t);

            bullet.Kill();
        }
    }
}