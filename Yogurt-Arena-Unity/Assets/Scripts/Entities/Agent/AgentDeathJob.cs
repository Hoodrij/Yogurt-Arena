using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct AgentDeathJob : IDeathJob
    {
        public async UniTask Run(Entity entity)
        {
            AgentConfig config = entity.Get<AgentConfig>();
            Vector3 position = entity.Get<BodyState>().Position;

            AgentDeathVFX vfx = await config.DeathVFX.Spawn();
            vfx.transform.position = position.AddY(0.5f);
        }
    }
}