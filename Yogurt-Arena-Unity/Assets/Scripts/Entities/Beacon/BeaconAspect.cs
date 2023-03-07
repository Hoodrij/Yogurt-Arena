using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BeaconDestination Destination => this.Get<BeaconDestination>();
        public BeaconView View => this.Get<BeaconView>();

        public Transform Transform => View.Transform;
    }
}