namespace Yogurt.Arena;

public struct BeaconMoveJob
{
    public void Run(BeaconAspect beacon)
    {
        beacon.Run(Update);
        return;

        void Update()
        {
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            BeaconConfig config = beacon.Config;
            ref BeaconBodyState body = ref beacon.Body;

            if (!inputField.Input.HasClick) 
                return;
            inputField.Input.HasClick = false;

            PlayerAspect player = Query.Single<PlayerAspect>();
            if (!player.Exist())
                return;
                
            Vector3 target = inputField.Input.ClickWorldPosition;
            body.RawDestination = target;
            body.Destination = CalcDestination(body.Destination, target);
            body.RawDestination = body.RawDestination.WithY(body.Destination.y);
            body.RawDestination = ClampRawDestination(body.RawDestination, body.Destination, config.Elasticity);

            new SpawnBeaconVfxJob().Run(config, body.Destination).Forget();

            return;

            Vector3 CalcDestination(Vector3 prevDest, Vector3 newDest)
            {
                int mask = NavMesh.AllAreas;
                NavMesh.SamplePosition(newDest, out var newHit, 100, mask);

                NavMeshPath path = new NavMeshPath();
                NavMesh.CalculatePath(prevDest, newHit.position, mask, path);

                if (path.status != NavMeshPathStatus.PathComplete)
                {
                    return CalcDestination(prevDest, newDest.WithY(10));
                }

                return path.corners.Last();
            }
            
            Vector3 ClampRawDestination(Vector3 rawDest, Vector3 dest, float elasticity)
            {
                float magnitude = (rawDest - dest).magnitude * (1/elasticity);
                return Vector3.Lerp(rawDest, dest, magnitude);
            }
        }
    }
}