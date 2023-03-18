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
            beacon.Body.AddDelta(moveDelta);

            Transform transform;
            (transform = beacon.View.transform).position = Vector3.Lerp(beacon.View.transform.position, beacon.Body.Destination, data.Beacon.SmoothValue);
            SpecifyTransformY(transform, beacon.Body);
        }
            
        private static void SpecifyTransformY(Transform transform, BeaconBodyState body)
        {
            Vector3 requiredPos = transform.position.WithY(body.Destination.y);
            if (NavMesh.SamplePosition(requiredPos, out var hit, 10, NavMesh.AllAreas))
            {
                transform.position = transform.position.WithY(hit.position.y);
            }
        }
    }
}