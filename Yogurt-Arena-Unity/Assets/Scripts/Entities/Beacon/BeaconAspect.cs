using UnityEngine;

namespace Yogurt.Arena
{
    public struct BeaconAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BeaconState State => this.Get<BeaconState>();
        public BeaconView View => this.Get<BeaconView>();

        public Transform Transform => View.Transform;
    }
}