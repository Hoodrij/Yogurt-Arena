using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public class BeaconState : IComponent
    {
        public Vector3 RawDestination;
        public Vector3 Destination;
        
        public void AddDelta(Vector2 delta)
        {
            Vector3 moveDelta = delta.ToV3XZ();
            if (moveDelta == Vector3.zero) return;
            
            Data data = Query.Single<Data>();

            RawDestination += moveDelta;
            Destination = CalcDestination(Destination, RawDestination);
            RawDestination = RawDestination.WithY(Destination.y);
            RawDestination = ClampRawDestination(RawDestination, Destination, data.Beacon.Elasticity);
        }

        private static Vector3 CalcDestination(Vector3 prevDest, Vector3 newDest)
        {
            int mask = NavMesh.AllAreas;
            NavMesh.SamplePosition(newDest, out var newHit, 50, mask);

            NavMeshPath path = new NavMeshPath();
            NavMesh.CalculatePath(prevDest, newHit.position, mask, path);

            if (path.status != NavMeshPathStatus.PathComplete)
            {
                return CalcDestination(prevDest, newDest.WithY(10));
            }

            return path.corners.Last();
        }
    
        private static Vector3 ClampRawDestination(Vector3 rawDest, Vector3 dest, float elasticity)
        {
            float magnitude = (rawDest - dest).magnitude * (1/elasticity);
            return Vector3.Lerp(rawDest, dest, magnitude);
        }
    }
}