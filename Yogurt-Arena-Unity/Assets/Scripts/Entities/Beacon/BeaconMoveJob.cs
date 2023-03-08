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
            beacon.Destination.AddDelta(moveDelta);

            beacon.Transform.position = Vector3.Lerp(beacon.Transform.position, beacon.Destination.Destination, data.Beacon.SmoothValue);
            SpecifyTransformY(beacon.Transform, beacon.Destination);
        }
            
        private static void SpecifyTransformY(Transform transform, BeaconDestination destination)
        {
            Vector3 requiredPos = transform.position.WithY(destination.Destination.y);
            if (NavMesh.SamplePosition(requiredPos, out var hit, 10, NavMesh.AllAreas))
            {
                transform.position = transform.position.WithY(hit.position.y);
            }
        }
    }
}