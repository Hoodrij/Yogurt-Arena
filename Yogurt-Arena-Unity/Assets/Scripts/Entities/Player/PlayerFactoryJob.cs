﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct PlayerFactoryJob
    {
        public async UniTask<Entity> Run()
        {
            AgentData data = Query.Single<Data>().Player;
            
            AgentAspect player = await new AgentSpawnJob().Run(data, Team.Green, Vector3.zero);
            player.Add<PlayerTag>();

            // player.Items.Add(await new RifleFactoryJob().Run(player));

            return player.Entity;
        }
    }
}