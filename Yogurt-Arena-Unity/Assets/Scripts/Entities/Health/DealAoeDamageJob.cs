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
                if (hit.transform.TryGetComponent(out EntityLink link))
                {
                    if (link.Entity == owner.Entity)
                        continue;
                    
                    new DealDamageJob().Run(link.Entity, damage.Damage);
                }
            }
        }
    }
}