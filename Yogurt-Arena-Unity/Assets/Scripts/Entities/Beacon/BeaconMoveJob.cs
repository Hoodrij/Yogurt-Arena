using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct BeaconMoveJob : IUpdateJob
    {
        public void Update()
        {
            Data data = Query.Single<Data>();
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            BeaconAspect beacon = Query.Single<BeaconAspect>();

            Vector2 moveDelta = inputField.Input.MoveDelta;
            beacon.State.AddDelta(moveDelta);

            beacon.Transform.position = Vector3.Lerp(beacon.Transform.position, beacon.State.Destination, data.Beacon.SmoothValue);
            SpecifyTransformY(beacon.Transform, beacon.State);
        }
            
        private static void SpecifyTransformY(Transform transform, BeaconState state)
        {
            Vector3 requiredPos = transform.position.WithY(state.Destination.y);
            if (NavMesh.SamplePosition(requiredPos, out var hit, 10, NavMesh.AllAreas))
            {
                transform.position = transform.position.WithY(hit.position.y);
            }
        }
    }
}