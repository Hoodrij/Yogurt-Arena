using UnityEngine;
using DG.Tweening;

namespace Yogurt.Arena
{
    public class BeaconBodyState : IComponent
    {
        public Vector3 RawDestination;
        public Vector3 Destination;
        public Sequence Animation;
    }
}