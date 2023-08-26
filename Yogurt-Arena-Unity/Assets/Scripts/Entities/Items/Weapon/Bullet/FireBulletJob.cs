using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct FireBulletJob
    {
        public void Run(BulletAspect bullet, Vector3 velocity)
        {
            BodyState ownerBody = bullet.Owner.Value.Body;
            Vector3 position = ownerBody.MiddlePoint;

            Transform transform = bullet.View.transform;
            transform.DOKill();
            transform.localScale = Vector3.one;
            bullet.Body.Position = transform.position = position;

            SetFireVelocity();
            
            bullet.View.Trail.Clear();
            return;


            void SetFireVelocity()
            {
                if (velocity == default)
                    return;
                
                bullet.Remove<Kinematic>();
                bullet.Body.Velocity = velocity;
            
                transform.rotation = Quaternion.LookRotation(velocity);
            }
        }
    }
}