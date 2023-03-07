using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconMoveJob : IUpdateJob
    {
        public void Update()
        {
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            BeaconAspect beacon = Query.Single<BeaconAspect>();

            beacon.Transform.position += new Vector3(inputField.Input.MoveDelta.x, 0, inputField.Input.MoveDelta.y);
            beacon.Destination.Destination = beacon.Transform.position;
            beacon.Destination.RawDestination = beacon.Transform.position;
        }
    }
}