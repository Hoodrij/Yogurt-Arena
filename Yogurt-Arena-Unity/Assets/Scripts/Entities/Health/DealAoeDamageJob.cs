using UnityEngine;

namespace Yogurt.Arena
{
    public struct DealAoeDamageJob
    {
        public void Run(AgentAspect owner, Vector3 origin, AoeDamage damage)
        {
            RaycastHit[] hits = Physics.SphereCastAll(origin, damage.Radius, Vector3.up, 0, damage.HitMask);
            
            foreach (RaycastHit hit in hits)
            {
                Entity entityHit = hit.GetEntity();
                if (entityHit == owner.Entity)
                    continue;
                
                new DealDamageJob().Run(entityHit, damage.Damage);
            }
        }
    }
}