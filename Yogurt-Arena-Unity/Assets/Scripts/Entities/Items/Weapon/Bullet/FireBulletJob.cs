using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct FireBulletJob
    {
        public void Run(BulletAspect bullet, Vector3 velocity)
        {
            BodyState ownerBody = bullet.State.Owner.Body;
            Vector3 position = ownerBody.Position.WithY(ownerBody.Position.y + 0.5f);
            
            bullet.View.transform.DOKill();
            bullet.View.transform.position = position;
            bullet.View.transform.localScale = Vector3.one;

            if (velocity != default)
            {
                bullet.Remove<Kinematic>();
                bullet.Body.Velocity = velocity;
                
                bullet.View.transform.rotation = Quaternion.LookRotation(velocity);
            }
            
            bullet.View.Trail.Clear();
        }
    }
}