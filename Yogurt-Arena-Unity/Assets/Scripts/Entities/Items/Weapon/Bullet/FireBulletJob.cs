using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct FireBulletJob
    {
        public void Run(BulletAspect bullet, Vector3 velocity)
        {
            BodyState ownerBody = bullet.State.Owner.Body;
            Vector3 position = ownerBody.Position.AddY(0.5f);

            Transform transform = bullet.View.transform;
            transform.DOKill();
            transform.localScale = Vector3.one;
            bullet.Body.Position = transform.position = position;

            if (velocity != default)
            {
                bullet.Remove<Kinematic>();
                bullet.Body.Velocity = velocity;
                
                transform.rotation = Quaternion.LookRotation(velocity);
            }
            
            bullet.View.Trail.Clear();
        }
    }
}