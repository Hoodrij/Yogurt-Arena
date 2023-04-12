using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public class BeaconBodyState : IComponent
    {
        public Vector3 RawDestination;
        public Vector3 Destination;
    }
}