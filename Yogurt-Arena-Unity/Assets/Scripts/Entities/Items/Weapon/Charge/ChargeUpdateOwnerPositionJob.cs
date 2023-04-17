using Cysharp.Threading.Tasks;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct ChargeUpdateOwnerPositionJob
    {
        public async void Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            while (bullet.Exist() && owner.Exist())
            {
                NavMesh.SamplePosition(bullet.View.transform.position, out var attackPositionHit, 100, NavMesh.AllAreas);
                owner.View.transform.position = owner.Body.Position = owner.Body.Destination = attackPositionHit.position;
                
                await UniTask.Yield();
            }
        }
    }
}