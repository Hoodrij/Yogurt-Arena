﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeUpdateBulletPositionJob
    {
        public async void Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            while (bullet.Exist() && owner.Exist())
            {
                Transform transform = bullet.View.transform;
                transform.position = owner.View.transform.position.AddY(0.5f);

                await UniTask.Yield();
            }
        }
    }
}