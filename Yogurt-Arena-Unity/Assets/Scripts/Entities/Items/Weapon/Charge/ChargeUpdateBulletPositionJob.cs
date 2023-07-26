using UnityEngine;

namespace Yogurt.Arena
{
    public struct ChargeUpdateBulletPositionJob
    {
        public async void Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.State.Owner;
            bullet.Run(Update);


            async Awaitable Update()
            {
                if (!owner.Exist())
                    return;
                
                Transform transform = bullet.View.transform;
                Vector3 position = owner.View.transform.position.AddY(0.5f);
                bullet.Body.Position = transform.position = position;
            }
        }
    }
}