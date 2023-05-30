using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForItemPickupJob
    {
        public async UniTask<AgentAspect> Run(ItemSpotAspect itemSpot)
        {
            RaycastHit[] hits = new RaycastHit[3];
            
            Vector3 position = itemSpot.Body.Position;
            LayerMask mask = itemSpot.State.Mask;

            while (itemSpot.Exist())
            {
                int hitsCount = Physics.SphereCastNonAlloc(position, itemSpot.State.Radius, Vector3.up, hits, 0.1f, mask);

                for (int i = 0; i < hitsCount; i++)
                {
                    RaycastHit hit = hits[i];
                    
                    if (!hit.transform.TryGetComponent(out EntityLink link))
                        continue;

                    return link.Entity.As<AgentAspect>();
                }
                
                await UniTask.Yield();
            }

            return default;
        }
    }
}