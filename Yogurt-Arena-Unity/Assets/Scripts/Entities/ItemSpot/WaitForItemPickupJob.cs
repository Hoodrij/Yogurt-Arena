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
            LayerMask mask = itemSpot.Config.Mask;
            AgentAspect result = default;

            await Wait.Until(IsPickedUp, itemSpot.Life());

            return result;


            bool IsPickedUp()
            {
                int hitsCount = Physics.SphereCastNonAlloc(position, itemSpot.Config.Radius, Vector3.up, hits, 0.1f, mask);

                for (int i = 0; i < hitsCount; i++)
                {
                    RaycastHit hit = hits[i];
                    Entity entity = hit.GetEntity();

                    if (!entity.Has<PlayerTag>())
                        continue;

                    result = entity.As<AgentAspect>();
                    return true;
                }

                return false;
            }
        }
    }
}