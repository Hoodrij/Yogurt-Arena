using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentDeathJob : IDeathJob
    {
        public async UniTask Run(Entity entity)
        {
            AgentConfig config = entity.Get<AgentConfig>();
            BodyState body = entity.Get<BodyState>();

            AgentDeathVFX vfx = await config.DeathVFX.Spawn();
            vfx.transform.position = body.MiddlePoint;
        }
    }
}